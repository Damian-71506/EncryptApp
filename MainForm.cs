using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System;

namespace App
{
    public partial class MainForm : Form
    {
        private byte[] key;
        private byte[] iv;
        private static readonly HttpClient httpClient = new HttpClient();

        public MainForm()
        {
            InitializeComponent();
            encryptFileButton.Click += EncryptFileButton_Click;
            decryptFileButton.Click += DecryptFileButton_Click;
            encryptFolderButton.Click += EncryptFolderButton_Click;
            decryptFolderButton.Click += DecryptFolderButton_Click;
            sendToServerButton.Click += SendToServerButton_Click;
        }

        private void SendToServerButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] filePaths = openFileDialog.FileNames;
                    Task.Run(() => {
                        foreach (string filePath in filePaths)
                        {
                            SendFileToServer(filePath);
                        }
                        UpdateStatus("Pomyślnie wysłano pliki na serwer.");
                    });
                }
            }
        }

        private async void SendFileToServer(string filePath)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file", Path.GetFileName(filePath));
                
                    var response = await httpClient.PostAsync("http://yourserver.com/upload", content); // Zastąp właściwym adresem URL swojego serwera. Upewnij się, że serwer jest skonfigurowany do odbierania plików przez POST.
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("Wystąpił błąd podczas wysyłania pliku: " + ex.Message);
            }
        }

        private void EncryptFileButton_Click(object sender, EventArgs e)
        {
            string algorithm = comboBox.SelectedItem.ToString();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string destinationFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + "_encrypted" + Path.GetExtension(filePath));

                    Task.Run(() => {
                        EncryptFile(filePath, destinationFilePath, algorithm);
                        UpdateStatus("Pomyślnie zaszyfrowano dane.");
                    });
                }
            }
        }

        private void DecryptFileButton_Click(object sender, EventArgs e)
        {
            string algorithm = comboBox.SelectedItem.ToString();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                    string fileExtension = Path.GetExtension(filePath);

                    if (fileNameWithoutExtension.EndsWith("_encrypted"))
                    {
                        fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.Length - "_encrypted".Length);
                    }

                    string destinationFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileNameWithoutExtension + "_decrypted" + fileExtension);

                    Task.Run(() => {
                        DecryptFile(filePath, destinationFilePath, algorithm);
                        UpdateStatus("Pomyślnie odszyfrowano dane.");
                    });
                }
            }
        }

        private void EncryptFolderButton_Click(object sender, EventArgs e)
        {
            string algorithm = comboBox.SelectedItem.ToString();
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    string destinationFolderPath = folderPath + "_encrypted";

                    Task.Run(() => {
                        EncryptFolder(folderPath, destinationFolderPath, algorithm);
                        UpdateStatus("Pomyślnie zaszyfrowano folder.");
                    });
                }
            }
        }

        private void DecryptFolderButton_Click(object sender, EventArgs e)
        {
            string algorithm = comboBox.SelectedItem.ToString();
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    string destinationFolderPath = folderPath;
                    if (folderPath.EndsWith("_encrypted"))
                    {
                        destinationFolderPath = folderPath.Substring(0, folderPath.Length - "_encrypted".Length) + "_decrypted";
                    }
                    else
                    {
                        destinationFolderPath += "_decrypted";
                    }

                    Task.Run(() => {
                        DecryptFolder(folderPath, destinationFolderPath, algorithm);
                        UpdateStatus("Pomyślnie odszyfrowano folder.");
                    });
                }
            }
        }

        private SymmetricAlgorithm GetAlgorithmByName(string algorithmName)
        {
            switch (algorithmName)
            {
                case "Aes":
                    return Aes.Create();
                case "DES":
                    return DES.Create();
                case "RC2":
                    return RC2.Create();
                case "Rijndael":
                    return Rijndael.Create();
                case "TripleDES":
                    return TripleDES.Create();
                default:
                    throw new ArgumentException("Nieznany algorytm.");
            }
        }

        private void EncryptFile(string inputFilePath, string outputFilePath, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithmByName(algorithmName))
            {
                algorithm.GenerateKey();
                algorithm.GenerateIV();

                key = algorithm.Key;
                iv = algorithm.IV;

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }

                File.WriteAllBytes(outputFilePath + ".key", key);
                File.WriteAllBytes(outputFilePath + ".iv", iv);
            }
        }

        private void DecryptFile(string inputFilePath, string outputFilePath, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithmByName(algorithmName))
            {
                key = File.ReadAllBytes(inputFilePath + ".key");
                iv = File.ReadAllBytes(inputFilePath + ".iv");

                algorithm.Key = key;
                algorithm.IV = iv;

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputFileStream);
                }
            }
        }

        private void EncryptFolder(string inputFolderPath, string outputFolderPath, string algorithmName)
        {
            Directory.CreateDirectory(outputFolderPath);

            foreach (string filePath in Directory.GetFiles(inputFolderPath))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(outputFolderPath, fileName + ".encrypted");

                EncryptFile(filePath, destFilePath, algorithmName);
            }
        }

        private void DecryptFolder(string inputFolderPath, string outputFolderPath, string algorithmName)
        {
            Directory.CreateDirectory(outputFolderPath);

            foreach (string filePath in Directory.GetFiles(inputFolderPath, "*.encrypted"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string destFilePath = Path.Combine(outputFolderPath, fileName);

                DecryptFile(filePath, destFilePath, algorithmName);
            }
        }

        private void UpdateStatus(string message)
        {
            if (labelInfo.InvokeRequired)
            {
                labelInfo.Invoke(new Action(() => labelInfo.Text = message));
            }
            else
            {
                labelInfo.Text = message;
            }
        }
    }
}

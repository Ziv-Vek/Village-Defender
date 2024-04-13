using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VillageDefender.Utils;

namespace VillageDefender.Services
{
    public class LocalSaveLoadService : Singleton<LocalSaveLoadService>, ILocalDataService
    {
        //TODO: move the key and iv to a secure place
        private const string KEY = "4hWTj124vPWe7zjg3a1zMQ==";
        private const string IV = "kbWU0s4aEJ43AWsZDtyFxQ==";

        public async Task<bool> SaveToFileAsync<T>(T data, string path, bool encrypt = false)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);

            try
            {
                Debug.Log(
                    File.Exists(fullPath) ? "Overwriting existing file: " + fullPath : "Writing new file: " + fullPath);

                string jsonData = JsonConvert.SerializeObject(data);

                if (encrypt)
                {
                    return await EncryptJson(fullPath, jsonData);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(fullPath, false))
                    {
                        await writer.WriteAsync(jsonData);
                    }

                    Debug.Log($"Saved successfuly to {fullPath}");
                    return true;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error saving data: " + e.Message);
                return false;
            }
        }

        private async Task<bool> EncryptJson(string fullPath, string jsonData)
        {
            try
            {
                using Aes aesProvider = Aes.Create();
                aesProvider.Key = Convert.FromBase64String(KEY);
                aesProvider.IV = Convert.FromBase64String(IV);
                using ICryptoTransform encryptor = aesProvider.CreateEncryptor();
                using FileStream fs = File.Create(fullPath);
                using CryptoStream cryptoStream = new CryptoStream(fs, encryptor, CryptoStreamMode.Write);

                byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
                await cryptoStream.WriteAsync(bytes, 0, bytes.Length);
                Debug.Log($"Encrypted and saved successfuly to {fullPath}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error during file encryption for path: ${fullPath}" + e.Message);
                return false;
            }
        }

        public async Task<T> LoadFromFileAsync<T>(string path, bool decrypt = false)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);

            try
            {
                Debug.Log("Loading data from: " + fullPath);

                if (!File.Exists(fullPath))
                {
                    Debug.LogError("File not found: " + fullPath);
                    throw new FileNotFoundException($"File not found: {fullPath}");
                }

                string json = string.Empty;
                if (decrypt)
                {
                    json = await DecryptJson(fullPath);
                }
                else
                {
                    json = await File.ReadAllTextAsync(fullPath);
                }

                Debug.Log("Loaded successfuly from: " + fullPath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to Load data from path {fullPath}" + e.Message);
                throw;
            }
        }

        private async Task<string> DecryptJson(string fullPath)
        {
            try
            {
                byte[] encryptedData = await File.ReadAllBytesAsync(fullPath);
                using Aes aesProvider = Aes.Create();
                aesProvider.Key = Convert.FromBase64String(KEY);
                aesProvider.IV = Convert.FromBase64String(IV);

                using ICryptoTransform decryptor = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
                using MemoryStream ms = new MemoryStream(encryptedData);
                await using CryptoStream cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader reader = new StreamReader(cryptoStream);

                Debug.Log("Decrypted and loaded successfuly from: " + fullPath);
                return await reader.ReadToEndAsync();
            }
            catch (IOException e)
            {
                Debug.LogError("Error during file decryption for path: " + fullPath + e.Message);
                throw;
            }
        }

        public bool DeleteFile(string path)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log("File deleted: " + fullPath);
                return true;
            }
            else
            {
                Debug.LogWarning($"Failed to delete file at {fullPath}.\n File not found");
                return false;
            }
        }

        public bool IsFileExists(string path)
        {
            return File.Exists(Path.Combine(Application.persistentDataPath, path));
        }
    }
}
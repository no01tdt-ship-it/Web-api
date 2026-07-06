using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Web_api.Helpers
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ chuyển đổi tiếng Việt không dấu, tạo slug, format dữ liệu, validate và mã hóa cơ bản.
    /// </summary>
    public static class SlugHelper
    {
        #region 1. Chuyển tiếng Việt có dấu thành không dấu
        /// <summary>
        /// Chuyển đổi chuỗi tiếng Việt có dấu thành không dấu.
        /// </summary>
        /// <param name="str">Chuỗi tiếng Việt cần chuyển đổi.</param>
        /// <returns>Chuỗi đã loại bỏ dấu tiếng Việt.</returns>
        public static string RemoveSign4VietnameseString(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            // Chuẩn hóa chuỗi unicode phân rã các dấu thanh ra khỏi chữ cái gốc (FormD)
            string normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                // Loại bỏ các ký tự dấu thanh (NonSpacingMark)
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    if (c == 'đ' || c == 'Đ')
                    {
                        stringBuilder.Append(c == 'đ' ? 'd' : 'D');
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }
                }
            }

            // Chuẩn hóa ngược lại về dạng kết hợp nguyên bản (FormC)
            string result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Bổ sung xử lý một số ký tự đặc biệt/dấu phụ khác nếu còn sót
            result = Regex.Replace(result, "[đđ]", "d", RegexOptions.IgnoreCase);

            return result;
        }
        #endregion

        #region 2. Tạo slug / mã code
        /// <summary>
        /// Tạo Slug thân thiện với đường dẫn URL từ một chuỗi bất kỳ (chuyển chữ thường, bỏ dấu tiếng Việt, nối bằng dấu gạch ngang).
        /// </summary>
        /// <param name="text">Chuỗi gốc cần tạo slug.</param>
        /// <returns>Chuỗi slug chuẩn URL.</returns>
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            // 1. Chuyển thành chữ thường và xóa dấu tiếng Việt
            string str = RemoveSign4VietnameseString(text).ToLowerInvariant();

            // 2. Loại bỏ các ký tự đặc biệt không phải chữ cái, chữ số hoặc khoảng trắng
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // 3. Thay thế nhiều khoảng trắng hoặc dấu gạch ngang liên tiếp bằng một dấu gạch ngang duy nhất
            str = Regex.Replace(str, @"\s+", "-").Trim();
            str = Regex.Replace(str, @"-+", "-");

            // 4. Loại bỏ dấu gạch ngang ở đầu và cuối chuỗi
            str = str.Trim('-');

            return str;
        }

        /// <summary>
        /// Tạo mã code chuẩn (chuyển chữ hoa, bỏ dấu tiếng Việt, nối bằng dấu gạch ngang).
        /// Thường dùng cho mã menu, mã danh mục, mã sản phẩm.
        /// </summary>
        /// <param name="text">Chuỗi gốc cần tạo mã code.</param>
        /// <returns>Chuỗi mã code chuẩn dạng UPPERCASE_WITH_HYPHENS.</returns>
        public static string GenerateCode(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            // 1. Chuyển chữ hoa và loại bỏ dấu tiếng Việt
            string str = RemoveSign4VietnameseString(text).ToUpperInvariant();

            // 2. Giữ lại chữ, số, khoảng trắng, gạch ngang, gạch dưới
            str = Regex.Replace(str, @"[^A-Z0-9\s_-]", "");

            // 3. Thay thế khoảng trắng và gạch dưới bằng gạch ngang (-)
            str = Regex.Replace(str, @"[\s_]+", "-").Trim();
            str = Regex.Replace(str, @"-+", "-");

            // 4. Cắt các ký tự gạch ngang ở đầu và cuối
            return str.Trim('-');
        }
        #endregion

        #region 3. Format ngày tháng
        /// <summary>
        /// Định dạng thời gian thành chuỗi ngày tháng năm tùy chỉnh.
        /// </summary>
        /// <param name="dateTime">Thời gian cần định dạng.</param>
        /// <param name="format">Định dạng (Mặc định: "dd/MM/yyyy HH:mm:ss").</param>
        /// <returns>Chuỗi ngày tháng đã định dạng.</returns>
        public static string FormatDateTime(DateTime dateTime, string format = "dd/MM/yyyy HH:mm:ss")
        {
            return dateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Định dạng thời gian nullable thành chuỗi ngày tháng năm tùy chỉnh.
        /// </summary>
        /// <param name="dateTime">Thời gian cần định dạng (nullable).</param>
        /// <param name="format">Định dạng (Mặc định: "dd/MM/yyyy HH:mm:ss").</param>
        /// <returns>Chuỗi ngày tháng đã định dạng hoặc rỗng nếu null.</returns>
        public static string FormatDateTime(DateTime? dateTime, string format = "dd/MM/yyyy HH:mm:ss")
        {
            return dateTime.HasValue ? dateTime.Value.ToString(format, CultureInfo.InvariantCulture) : string.Empty;
        }
        #endregion

        #region 4. Format tiền tệ
        /// <summary>
        /// Định dạng số tiền thành chuỗi hiển thị theo đơn vị tiền tệ VNĐ (ví dụ: 100.000 ₫).
        /// </summary>
        /// <param name="amount">Số tiền cần định dạng.</param>
        /// <returns>Chuỗi tiền tệ đã định dạng kèm ký hiệu ₫.</returns>
        public static string FormatVND(decimal amount)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:c0}", amount);
        }

        /// <summary>
        /// Định dạng số tiền thành chuỗi hiển thị theo đơn vị tiền tệ VNĐ (nullable).
        /// </summary>
        public static string FormatVND(decimal? amount)
        {
            return amount.HasValue ? FormatVND(amount.Value) : "0 ₫";
        }
        #endregion

        #region 5. Validate số điện thoại
        /// <summary>
        /// Kiểm tra số điện thoại Việt Nam hợp lệ (10 số, bắt đầu bằng 0 hoặc +84 kết hợp với các đầu số 3, 5, 7, 8, 9).
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại cần kiểm tra.</param>
        /// <returns>True nếu hợp lệ, ngược lại trả về False.</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;

            // Biểu thức Regex kiểm tra số điện thoại di động Việt Nam phổ biến
            const string phoneRegex = @"^(0|\+84)(3|5|7|8|9)[0-9]{8}$";
            return Regex.IsMatch(phoneNumber.Trim(), phoneRegex);
        }
        #endregion

        #region 6. Tạo mã ngẫu nhiên
        /// <summary>
        /// Tạo một chuỗi ký tự ngẫu nhiên (chữ và số).
        /// </summary>
        /// <param name="length">Độ dài chuỗi ngẫu nhiên cần tạo (Mặc định là 8).</param>
        /// <returns>Chuỗi ngẫu nhiên.</returns>
        public static string GenerateRandomCode(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region 7. Mã hóa / giải mã đơn giản
        /// <summary>
        /// Mã hóa chuỗi ký tự sang chuẩn Base64.
        /// </summary>
        /// <param name="plainText">Chuỗi văn bản gốc.</param>
        /// <returns>Chuỗi mã hóa Base64.</returns>
        public static string Base64Encode(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Giải mã chuỗi Base64 về văn bản gốc.
        /// </summary>
        /// <param name="base64EncodedData">Chuỗi mã hóa Base64.</param>
        /// <returns>Văn bản gốc sau khi giải mã.</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData)) return string.Empty;
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                return string.Empty; // Trả về rỗng nếu chuỗi mã hóa không đúng định dạng Base64
            }
        }

        /// <summary>
        /// Mã hóa chuỗi ký tự sử dụng thuật toán mã hóa đối xứng AES (256-bit).
        /// </summary>
        /// <param name="plainText">Văn bản gốc cần mã hóa.</param>
        /// <param name="secretKey">Khóa bí mật (Mặc định sử dụng khóa hệ thống nếu không truyền vào).</param>
        /// <returns>Chuỗi mã hóa dạng Base64.</returns>
        public static string EncryptAes(string plainText, string secretKey = "Web_Api_Secret_Encryption_Key_2026")
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            byte[] iv = new byte[16]; // Vector khởi tạo mặc định bằng 0
            byte[] encryptedBytes;

            using (Aes aes = Aes.Create())
            {
                // Đảm bảo khóa bí mật có độ dài chuẩn 256-bit (32 bytes)
                aes.Key = Encoding.UTF8.GetBytes(secretKey.PadRight(32).Substring(0, 32));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        encryptedBytes = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Giải mã chuỗi ký tự đã mã hóa bằng AES (256-bit).
        /// </summary>
        /// <param name="cipherText">Chuỗi đã mã hóa dạng Base64.</param>
        /// <param name="secretKey">Khóa bí mật dùng để mã hóa trước đó.</param>
        /// <returns>Văn bản gốc sau giải mã.</returns>
        public static string DecryptAes(string cipherText, string secretKey = "Web_Api_Secret_Encryption_Key_2026")
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;

            try
            {
                byte[] iv = new byte[16];
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(secretKey.PadRight(32).Substring(0, 32));
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return string.Empty; // Trả về rỗng nếu giải mã thất bại (sai khóa hoặc sai dữ liệu mã hóa)
            }
        }
        #endregion
    }
}

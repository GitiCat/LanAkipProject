using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Akip.NetStream
{
    public static class Request
    {
        /// <summary>
        ///     Предоставляет объект кодировки
        /// </summary>
        private static Encoding encoding = Encoding.ASCII;

        /// <summary>
        ///     Предоставляет метод для отправки запроса по локальному подключению
        /// </summary>
        /// <param name="networkStream">Поток связи активного подлкючения</param>
        /// <param name="message">Сообщени для запроса</param>
        public static void SetSystemValue(NetworkStream networkStream, string message)
        {
            if (networkStream.CanWrite)
            {
                byte[] MessageByteArray = encoding.GetBytes(message);
                networkStream.Write(MessageByteArray, 0, MessageByteArray.Length);
            }
            else
            {
                MessageBox.Show("Подключение не поддерживает операции записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        ///     Предоставляет метод для чтения потока активного локального подключения
        /// </summary>
        /// <param name="networkStream">Поток связи активного подключения</param>
        /// <returns></returns>
        public static string GetSystemValue(NetworkStream networkStream)
        {
            //  Буфер приема данных
            byte[] readBuffer = new byte[1024];
            //  Строитель полученных данных
            StringBuilder completeMessage = new StringBuilder();
            //  Количество полученных данных
            int numberOfBytesRead = 0;

            try
            {
                if (networkStream.CanRead)
                {
                    do
                    {
                        //  Чтение данных
                        numberOfBytesRead = networkStream.Read(readBuffer, 0, readBuffer.Length);
                        //  Преобразование полученных данных в строку
                        completeMessage.AppendFormat("{0}", encoding.GetString(readBuffer, 0, numberOfBytesRead));
                    } while (networkStream.DataAvailable);
                }
                else
                {
                    MessageBox.Show("Подключение не поддерживает операцию чтения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return string.Empty;
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }

            return completeMessage.ToString();
        }
    }
}

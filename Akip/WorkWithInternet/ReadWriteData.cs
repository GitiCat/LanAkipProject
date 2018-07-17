using System.Text;

namespace Akip
{
    public class ReadWriteData
    {
        public static void WriteToSerialData(string command)
        {
            byte[] b_data = Encoding.ASCII.GetBytes( command );

        }
    }
}

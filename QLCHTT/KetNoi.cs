using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHTT_WebForms_Frontend
{
    public class KetNoi
    {
        // Chuỗi kết nối SQL Server
        private string strKetNoi =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True;Encrypt=False";

        // Đối tượng kết nối
        public SqlConnection conn;

        // Hàm khởi tạo
        public KetNoi()
        {
            conn = new SqlConnection(strKetNoi);
        }

        // Hàm mở kết nối
        public void MoKetNoi()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        // Hàm đóng kết nối
        public void DongKetNoi()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        // Hàm lấy đối tượng kết nối
        public SqlConnection GetConnection()
        {
            return conn;
        }
    }
}

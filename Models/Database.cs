using System.Data.SQLite;
namespace testt.Models
{
    public class Database
    {
        private SQLiteConnection connection;
        private string databaseName = "Database.db";

        public Database()
        {
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName}; Version=3;");
            connection.Open();
        }
        public User GetUsers(User model)
        {
            User users = null;
                string query = "SELECT * FROM Users WHERE UserName=@UserName AND PassWord=@PassWord";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Name = reader.GetString(3);
                            users.Email = reader.GetString(4);
                            users.Phone = reader.GetString(5);
                            users.Role = reader.GetString(6);
                            users.Status = reader.GetString(7);
                            users.Address = reader.GetString(8);
                            users.Sex = reader.GetString(9);
                        string role = reader.GetString(6); // Ở đây, giả sử cột role có chỉ số là 5
                            if (role == "admin")
                            {
                                // Người dùng có vai trò là admin
                                users.Role = "admin";
                            }
                            else
                            {
                                // Người dùng có vai trò khác
                                users.Role = "user";
                            }
                        }
                    }
                }

 

            return users;
        }
        public User CheckUsers(User model)
        {
            User users = null;
                string query = "SELECT * FROM Users WHERE UserName=@UserName";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.UserName = reader.GetString(1);
                        }
                    }
                }

            return users;
        }
        public void AddUser(User model)
        {
                string query = "INSERT INTO Users (Name,UserName, PassWord,Phone, Email, Role,Status,Address,Sex,NgayThue,NgayTra) VALUES (@Name,@UserName, @PassWord,@Phone, @Email, @Role,@Status,@Address,@Sex,@NgayThue,@NgayTra)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    command.Parameters.AddWithValue("@Phone", model.Phone);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Role", "user");
                    command.Parameters.AddWithValue("@Status", "Chưa thuê");
                    command.Parameters.AddWithValue("@Address", " ");
                    command.Parameters.AddWithValue("@Sex", " ");
                    command.Parameters.AddWithValue("@NgayThue", " ");
                    command.Parameters.AddWithValue("@NgayTra", " ");
                    command.ExecuteNonQuery();
                }
        }
        public void Themkh(User model)
        {
            string query = "INSERT INTO Users (Name,UserName,Password,Phone, Email,Address,Status,Sex, Role,NgayThue,NgayTra) VALUES (@Name,@UserName,@Password,@Phone,  @Email,@Address,@Status,@Sex, @Role,@NgayThue,@NgayTra)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@UserName", " ");
                command.Parameters.AddWithValue("@Password", " ");
                command.Parameters.AddWithValue("@Phone", model.Phone);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Status", "Chưa thuê");
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Sex", model.Sex);
                command.Parameters.AddWithValue("@Role", "user");
                command.Parameters.AddWithValue("@NgayThue", " ");
                command.Parameters.AddWithValue("@NgayTra", " ");
                command.ExecuteNonQuery();
            }
        }
        public List<User> LoadUser()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM Users";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        PassWord = reader.GetString(2),
                        Name = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        Role = reader.GetString(6),
                        Status = reader.GetString(7),
                        Address = reader.GetString(8),
                        Sex = reader.GetString(9),
                        NgayThue = reader.GetString(10),
                        NgayTra = reader.GetString(11)
                    });
                }
            }
            return users;
        }
        public User GetUsers(int id)
        {
            User users = new User();
                string query = "SELECT * FROM Users WHERE ID=@ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Name = reader.GetString(3);
                            users.Email = reader.GetString(4);
                            users.Phone = reader.GetString(5);
                            users.Role = reader.GetString(6);
                            users.Status = reader.GetString(7);
                            users.Address = reader.GetString(8);
                            users.Sex = reader.GetString(9);
                            users.NgayThue = reader.GetString(10);
                            users.NgayTra = reader.GetString(11);
                        }
                    }
                }
            return users;
        }
        public void saveUser(User model)
        {
                string query = "UPDATE Users SET Email=@email, Phone=@mobile,Name=@name,Address=@address,Status=@status,Sex=@sex,NgayThue=@NgayThue,NgayTra=@NgayTra WHERE ID=@id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", model.ID);
                    command.Parameters.AddWithValue("@mobile", model.Phone);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@status", model.Status);
                    command.Parameters.AddWithValue("@sex", model.Sex);
                    if(model.NgayThue == null)
                        {
                        command.Parameters.AddWithValue("@NgayThue"," ");
                    }else
                        command.Parameters.AddWithValue("@NgayThue", model.NgayThue);
                    if (model.NgayTra == null)
                    {
                        command.Parameters.AddWithValue("@NgayTra", " ");
                    }
                    else
                        command.Parameters.AddWithValue("@NgayTra", model.NgayTra);

                    command.ExecuteNonQuery();
                }

        }
        public void addlich(string phone,string ngaythue,string ngaytra)
        {
            string query = "INSERT INTO lichsu (sdt,ngaythue,ngaytra) VALUES (@Phone, @ngaythue,@ngaytra)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@ngaythue", ngaythue);
                command.Parameters.AddWithValue("@ngaytra", ngaytra);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteUser(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE ID = @ID";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
        public void ThemPhong(Tro model)
        {
            string query = "INSERT INTO phongtro (ten,dientich,giatien,diachi,anh,sdt,mota) VALUES" +
                " (@ten,@dientich,@giatien,@diachi, @anh, @sdt,@mota)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ten", model.ten);
                command.Parameters.AddWithValue("@dientich", model.dientich);
                command.Parameters.AddWithValue("@giatien", model.giatien);
                command.Parameters.AddWithValue("@diachi", model.diachi);
                command.Parameters.AddWithValue("@sdt", model.sdt);
                command.Parameters.AddWithValue("@anh", model.anh);
                command.Parameters.AddWithValue("@mota", model.mota);
                command.ExecuteNonQuery();
            }
        }
        public List<Tro> GetPhong()
        {
            string query = "SELECT * FROM phongtro";
            List<Tro> tro = new List<Tro>();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] imageData = reader["anh"] as byte[];
                    tro.Add(new Tro
                    {
                        Id = reader.GetInt32(0),
                        ten = reader.GetString(1),
                        dientich = reader.GetString(2),
                        giatien = reader.GetString(3),
                        diachi = reader.GetString(4),
                        anh = imageData,
                        sdt = reader.GetString(6),
                        mota = reader.GetString(7)
                    });
                }
            return tro;
        }
        public byte[] GetAnh(int id)
        {
            string query = "SELECT * FROM phongtro WHERE ID=@ID";
            byte[] imageData = null;
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    imageData = reader["anh"] as byte[];
                }
            return imageData;
        }
        public Tro DetailPhong(int id)
        {
            string query = "SELECT * FROM phongtro WHERE ID=@ID";
            Tro tro = new Tro();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] imageData = reader["anh"] as byte[];
                        tro.Id = reader.GetInt32(0);
                        tro.ten = reader.GetString(1);
                        tro.dientich = reader.GetString(2);
                        tro.giatien = reader.GetString(3);
                        tro.diachi = reader.GetString(4);
                        tro.anh = imageData;
                        tro.sdt = reader.GetString(6);
                        tro.mota = reader.GetString(7);
                    }
                }
            }

            return tro;
        }
        public void DeletePhong(int id)
        {
            string deleteQuery = "DELETE FROM phongtro WHERE ID = @ID";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
    }

}


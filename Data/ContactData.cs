using MVCPracticaArqdSoft.Models;
using System.Data.SqlClient;

namespace MVCPracticaArqdSoft.Data {

    public class ContactData {
        private string connectionString = string.Empty;

        public ContactData () {
            connectionString = new Conexion().GetConnectionString();
        }

        public bool SaveOne (ContactModel newContact) {
            try {
                using (SqlConnection connection = new (connectionString)) {
                    connection.Open();

                    SqlCommand command = new("spContact_saveOne", connection) {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("contactName", newContact.Name);
                    command.Parameters.AddWithValue("phoneNumber", newContact.PhoneNumber);
                    command.Parameters.AddWithValue("contactEmail", newContact.Email ?? "SIN CORREO ELECTRÓNICO");
                    command.Parameters.AddWithValue("contactPassword", newContact.Password);

                    command.ExecuteNonQuery();

                    connection.Close();

                    return true;
                }
            } catch (Exception e) {
                string msg = e.Message;
                Console.WriteLine(msg);
                return false;
            }
        }
        
        public List<ContactModel> FindAll () {
            List<ContactModel> contacts = new ();

            using (SqlConnection connection = new (connectionString)) {

                connection.Open();
                SqlCommand command = new ("spContact_findAll", connection) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read()) {
                    contacts.Add(new() {
                        Id = Convert.ToInt32(dataReader["ContactId"]),
                        Name = dataReader["ContactName"].ToString(),
                        PhoneNumber = dataReader["PhoneNumber"].ToString(),
                        Email = dataReader["ContactEmail"].ToString(),
                        Password = dataReader["ContactPassword"].ToString()
                    });
                }

                connection.Close();
                dataReader.Close();
            }

            return contacts;
        }

        public ContactModel FindOne (int contactId) {
            ContactModel contact = new ();
            
            using (SqlConnection connection = new (connectionString)) {

                connection.Open();
                SqlCommand command = new("spContact_FindOne", connection) {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("contactId", contactId);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read()) {
                    contact.Id = Convert.ToInt32(dataReader["ContactId"]);
                    contact.Name = dataReader["ContactName"].ToString();
                    contact.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    contact.Email = dataReader["ContactEmail"].ToString();
                    contact.Password = dataReader["ContactPassword"].ToString();
                }

                connection.Close();
                dataReader.Close();
            }

            return contact;
        }

        public bool UpdateOne (int contactId, ContactModel newData) {
            ContactModel currentData = FindOne(contactId); // current contact information

            if (currentData == null) return false; // if the contact doesn't exist

            newData.Name ??= currentData.Name;
            newData.PhoneNumber ??= currentData.PhoneNumber;
            newData.Email ??= currentData.Email;
            newData.Password ??= currentData.Password;

            newData.Email = newData.Email == "" ? "SIN CORREO ELECTRÓNICO" : newData.Email;

            try {
                using (SqlConnection connection = new (connectionString)) {

                    connection.Open();
                    SqlCommand command = new("spContact_updateOne", connection) {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("contactId", contactId);
                    command.Parameters.AddWithValue("contactName", newData.Name);
                    command.Parameters.AddWithValue("phoneNumber", newData.PhoneNumber);
                    command.Parameters.AddWithValue("contactEmail", newData.Email);
                    command.Parameters.AddWithValue("contactPassword", newData.Password);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected == 1; // if everthing is fine
                }

            } catch (Exception e) {
                string msg = e.Message;
                Console.WriteLine(msg);
                return false; // if it can't be updated
            }
        }

        public bool DeleteOne (int contactId) {
            try {
                using (SqlConnection connection = new (connectionString)) {
                    connection.Open();

                    SqlCommand command = new("spContact_deleteOne", connection) {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("contactId", contactId);
                    command.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }

            } catch (Exception e) {
                string msg = e.Message;
                Console.WriteLine(msg);
                return false;
            }
        }
    }

}

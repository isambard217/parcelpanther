using System;
using PantherParcel.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PantherParcel.Database {
    
    public class DataManager {

        public string connectionString;

        public DataManager (string ConnectionString) {

            connectionString = ConnectionString;
        }


        public int InsertErrorMessage(Error error) {

            int result = 0;

            try {
         

            string query = @"INSERT INTO parcelpanther.Errors
                            (`Message`,
                             `Class`,
                             `Method`,
                             `CreateDateTime`
                             )
                             VALUES(@Message, @Class, @Method, @CreateDateTime);";


            using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString)) {

                mysqlconnection.Open();

                MySqlCommand command = new MySqlCommand(query, mysqlconnection);

                command.Parameters.AddWithValue("@Message", error.Message);
                command.Parameters.AddWithValue("@Class", error.Class);
                command.Parameters.AddWithValue("@Method", error.Method);
                command.Parameters.AddWithValue("@CreateDateTime", DateTime.Now);

                result = command.ExecuteNonQuery();

                return result;

            }

            } catch (Exception e) {

                Console.WriteLine(e.Message);
            }

            return result;

        }


        public int InsertParcels(ParcelDetails Parcel, long parcelEnquiryId) {
                
            int result = 0;

			try {

                    string query = @"INSERT INTO Parcels
                                (`Length`,
                                 `Width`,
                                 `Height`,
                                 `Weight`,
                                 `ParcelEnquiriesId`
                                 )
                                 VALUES(@Length, @Width, @Height, @Weight, @ParcelEnquiriesId);";


                    using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString)) {

                        mysqlconnection.Open();

                        MySqlCommand command = new MySqlCommand(query, mysqlconnection);

                        command.Parameters.AddWithValue("@Length", Parcel.length);
                        command.Parameters.AddWithValue("@Width", Parcel.width);
                        command.Parameters.AddWithValue("@Height", Parcel.height);
                        command.Parameters.AddWithValue("@Weight", Parcel.weight);
                        command.Parameters.AddWithValue("@ParcelEnquiriesId", parcelEnquiryId);

                        result = command.ExecuteNonQuery();

                        return result;

                    }

				

                } catch (Exception e) {
                
                // Set-up logging errors to the Database

				    
                    
                }
            return result;

       }


       public long SaveEnquiry(ParcelEnquiry parcelEnquiry) {

            // bool result = false;
            long result = 0;
            //Dictionary<bool, int> result = new Dictionary<bool, int>();

            try {
                
                string query = @"INSERT INTO ParcelEnquiries
                                (`FromCountry`,
                                 `FromAddress`,
                                 `ToCountry`,
                                 `ToAddress`,
                                 `Phone`,
                                 `NumberOfParcels`,
                                 `ParcelDescription`,
                                 `CreateDateTime`
                                 )
                                 VALUES(@FromCountry, @FromAddress, @ToCountry, @ToAddress, @Phone, @NumberOfParcels, @ParcelDescription, @createDatetime); SELECT LAST_INSERT_ID();";


                    using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString)) {

                            mysqlconnection.Open();

                            MySqlCommand command = new MySqlCommand(query, mysqlconnection);

                            command.Parameters.AddWithValue("@FromCountry", parcelEnquiry.fromCountry);
                            command.Parameters.AddWithValue("@FromAddress", parcelEnquiry.fromAddress);
                            command.Parameters.AddWithValue("@ToCountry", parcelEnquiry.toCountry);
                            command.Parameters.AddWithValue("@ToAddress", parcelEnquiry.toAddress);
                            command.Parameters.AddWithValue("@Phone", parcelEnquiry.contactNumber);
                            command.Parameters.AddWithValue("@NumberOfParcels", parcelEnquiry.parcelQuantity);
                            command.Parameters.AddWithValue("@ParcelDescription", parcelEnquiry.parcelDescription);
                            command.Parameters.AddWithValue("@createDatetime", DateTime.Now);

                            int indictor = command.ExecuteNonQuery();

                            result = command.LastInsertedId;

                    }

            } catch (Exception e) {


                Error error = new Error() { Class = "DataManager", Message = e.Message, Method = "SaveEnquiry" };

                InsertErrorMessage(error);

                Console.WriteLine(e.Message);

            }

            return result;

        } // End method 


    }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace iDomApiTest.models
{
    public class Employee
    {
        static string EMPLOYEE_ID     = "employee_id"   ;
        static string FIRST_NAME      = "first_name"    ;
        static string LAST_NAME       = "last_name"     ;
        static string DATE_OF_BIRTH   = "date_of_birth" ;
        static string PHONE_NUMBER    = "phone_number"  ;
        
        int        employeeId  ;
        string     firstName   ;
        string     lastName    ;
        DateTime   dateOfBirth ;
        string     phoneNumber ;

        public int EmployeeId
        {
            get => employeeId;
            set => employeeId = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => phoneNumber = value;
        }

        
        public static Employee FromJson(JObject value)
        {
            Employee e =  new Employee();
            
            if (value.Property("employeeId"  ) != null) e.employeeId  =(int)        value["employeeId"  ];
            if (value.Property("firstName"   ) != null) e.firstName   =(string)     value["firstName"   ];
            if (value.Property("lastName"    ) != null) e.lastName    =(string)     value["lastName"    ];
            if (value.Property("dateOfBirth" ) != null) e.dateOfBirth =(DateTime)   value["dateOfBirth" ];
            if (value.Property("phoneNumber" ) != null) e.phoneNumber =(string)     value["phoneNumber" ];

            return e;
        }

        public Employee(int employeeId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
        {
            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
        }
        
        public Employee()
        {
            this.employeeId  = 0;
            this.firstName   = "";
            this.lastName    = "";
            this.dateOfBirth = new DateTime();
            this.phoneNumber = "";
        }

        public static List<Employee> getAll()
        {
            List<Employee> employees = new List<Employee>();

             DBConnection db = DBConnection.Instance();

            if (db.IsConnect())
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");

                  
                    string sql = "SELECT employee_id, first_name, last_name, date_of_birth, phone_number FROM emploies_mgr.employees";
                    MySqlCommand cmd = new MySqlCommand(sql, db.Connection);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Console.WriteLine(rdr[0]+" -- "+rdr[1]);
                        Employee e =  new Employee();
                        e. employeeId   = (int)    rdr[EMPLOYEE_ID   ];
                        e. firstName    = (string) rdr[FIRST_NAME    ];
                        e. lastName     = (string) rdr[LAST_NAME     ];
                        e. dateOfBirth= (DateTime)   rdr[DATE_OF_BIRTH ];
                        e. phoneNumber  = (string) rdr[PHONE_NUMBER  ];

                        employees.Add(e);
                    }
                    rdr.Close();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
                db.Close();
            }

            return employees;
        }
        
        public static Employee get(int id)
        {
            Employee e = new Employee();

            DBConnection db = DBConnection.Instance();

            if (db.IsConnect())
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");

                  
                    string sql = "SELECT employee_id, first_name, last_name, date_of_birth, phone_number FROM emploies_mgr.employees WHERE employee_id = " + id;
                    MySqlCommand cmd = new MySqlCommand(sql, db.Connection);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    
                    rdr.Read();

                        //Console.WriteLine(rdr[0]+" -- "+rdr[1]);
                        
                        e. employeeId   = (int)rdr[EMPLOYEE_ID   ];
                        e. firstName    = (string) rdr[FIRST_NAME    ];
                        e. lastName     = (string) rdr[LAST_NAME     ];
                        e. dateOfBirth  = (DateTime)rdr[DATE_OF_BIRTH ];
                        e. phoneNumber  = (string) rdr[PHONE_NUMBER  ];

                        
                    
                    rdr.Close();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                db.Close();
            }

            return e;
        }


        public static string Create(Employee e)
        {

            String result= "{ \"status\" :\"error\" }";
            
            DBConnection db = DBConnection.Instance();

            if (db.IsConnect())
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");


                    string sql =
                        "INSERT INTO emploies_mgr.employees (employee_id, first_name, last_name, date_of_birth, phone_number)" +
                        "VALUES (" +
                        "'" + e.employeeId + "', " +
                        "'" + e.FirstName + "', " +
                        "'" + e.LastName + "', " +
                        "'" + String.Format("{0:yyyy-MM-dd}",         e.DateOfBirth) + "', " +
                        "'" + e.PhoneNumber + "')";
                 
                    MySqlCommand cmd = new MySqlCommand(sql, db.Connection);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result= "{ \"status\" :\"ok\" }";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                db.Close();
            }

            return result;
        }
        
        
        public static string Update(int id, Employee e)
        {

            String result= "{ \"status\" :\"error\" }";
            
            DBConnection db = DBConnection.Instance();

            if (db.IsConnect())
            {
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    string sql =
                        "UPDATE emploies_mgr.employees SET " +
                        "first_name='" + e.FirstName + "', " +
                        "last_name='" + e.LastName + "', " +
                        "date_of_birth='" + String.Format("{0:yyyy-MM-dd}",         e.DateOfBirth) + "', " +
                        "phone_number='" + e.PhoneNumber + "' WHERE employee_id = '"+id+"'";
                 
                    MySqlCommand cmd = new MySqlCommand(sql, db.Connection);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result= "{ \"status\" :\"ok\" }";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                db.Close();
            }

            return result;
        }

        public static string Delete(int id)
        {
                        String result= "{ \"status\" :\"error\" }";
                        
                        DBConnection db = DBConnection.Instance();
            
                        if (db.IsConnect())
                        {
                            try
                            {
                                Console.WriteLine("Connecting to MySQL...");
                                string sql =
                                    "DELETE FROM emploies_mgr.employees WHERE employee_id = '"+id+"'";
                             
                                MySqlCommand cmd = new MySqlCommand(sql, db.Connection);
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    result= "{ \"status\" :\"ok\" }";
                                }
            
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            db.Close();
                        }
            
                        return result;
        }
    }
}
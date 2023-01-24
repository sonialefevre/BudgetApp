using DAL;
var db = new MyDal();
Console.WriteLine("Number of users after creattion: " + db.Users.Count());

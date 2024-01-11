using System;
using System.Text.RegularExpressions;


public class User // genel kullanıcı özelliklerini ve kimlik doğrulama işlevselliğini içerir.
{
    protected string Username; // iki korunan üye değişkeni 
    protected string Password; 

    public User(string username, string password) // nesne oluşturulduğunda çalışan metod
    {
        Username = username;
        Password = password;
    }

    public bool Authenticate(string enteredUsername, string enteredPassword)
    // girilen kullanıcı adı ve şifreyi mevcut kullanıcı bilgileriyle karşılaştırarak kimlik doğrulama işlemini gerçekleştirir.
    {
        return Username == enteredUsername && Password == enteredPassword;
    }
}

public class AdminUser : User
{
    public AdminUser(string username, string password) : base(username, password)
    // türetilen sınıfın üst sınıfının kurucu metodunu çağırmak amacıyla kullanılır.
    {
    }
    // Gerekirse admin kullanıcılara özgü ek metodlar veya özellikler ekleyebilirsiniz.
}

public class CustomerUser : User
{
    public CustomerUser(string username, string password) : base(username, password)
    // türetilen sınıfın üst sınıfının kurucu metodunu çağırmak amacıyla kullanılır.
    {
    }
    // Gerekirse admin kullanıcılara özgü ek metodlar veya özellikler ekleyebilirsiniz.
}


public class Program
{
    static AdminUser adminUser = new AdminUser("admin", "123");
    static CustomerUser customerUser = new CustomerUser("user", "123");

    static double dizel = 40, benzin = 38, lpg = 17;
    static double dizeltank = 1200, benzintank = 1000, lpgtank = 900, saleAmount = 0;

    static void Main()
    {
        Console.WriteLine("Yakıt Satış Takip Uygulaması\n");

        // Kullanıcı ve yönetici girişi
        LoginMenu:
        Console.Write("Kullanıcı adınızı giriniz: ");
        string username = Console.ReadLine();
        Console.Write("Şifrenizi giriniz: ");
        string password = sifeliyaz();
        //Console.WriteLine("\nAdınız = {0} Şifreniz = {1}", username, password);
        //Console.ReadKey();

        if (IsAdmin(username, password))
        {
            AdminMenu();
        }
        else if (IsUser(username, password))
        {
            UserMenu();
        }
        else
        {
            Console.WriteLine("Geçersiz kullanıcı adı veya şifre. Tekrar deneyin.");
            goto LoginMenu; // farklı şeyler kullanmak için yaptım
        }
    }

    private static string sifeliyaz()
    {
        string pass = "";
        do
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                pass += key.KeyChar;
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, (pass.Length - 1));
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        } while (true);
        return pass;
    }

    static bool IsAdmin(string username, string password)
    {
        // adminUser örnekleri üzerinden kontrol edecek 
        return adminUser.Authenticate(username, password);
    }

    static bool IsUser(string username, string password)
    {
        return customerUser.Authenticate(username, password);
    }

    static void AdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin girişi başarılı!");
        Console.ReadKey();
        Console.Clear();

        int anaMenu = 0;

        do
        {
            Console.WriteLine("Yakıt Satış Takip Uygulaması\n");
            Console.WriteLine("1- Yakıt Fiyatları");
            Console.WriteLine("2- Fiyat Güncelleme");
            Console.WriteLine("3- Yakıt Satışı Yap");
            Console.WriteLine("4- Yakıt deposunun doluluk oranını göster");
            Console.WriteLine("5- Toplam Satışı Göster");
            Console.WriteLine("0- Çıkış");

            Console.Write("Seçiminiz: ");
            anaMenu = Convert.ToInt32(Console.ReadLine());

            switch (anaMenu)
            {
                case 1:
                    ShowFuelPrices();
                    break;
                case 2:
                    UpdateFuelPrices();
                    break;
                case 3:
                    PerformFuelSale();
                    break;
                case 4:
                    ShowTankStatus();
                    break;
                case 5:
                    ShowTotalSales();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }

        } while (anaMenu != 9);//döngünün sonsuz olarak dönmesini sağlar
    }

    static void ShowFuelPrices()
    {
        Console.Clear();
        Console.WriteLine("--- Yakıt Fiyatları ---");
        Console.WriteLine($"Dizel : {dizel} TL/litre");
        Console.WriteLine($"Benzin : {benzin} TL/litre");
        Console.WriteLine($"LPG : {lpg} TL/litre");

        Console.WriteLine("1- Ana Menüye dön\n0- Çıkış");
        int altMenu = Convert.ToInt32(Console.ReadLine());

        if (altMenu == 1)
        {
            Console.Clear();
        }
        else if (altMenu == 0)
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Geçersiz seçim. Ana menüye dönülüyor.");
            Console.Clear();
        }
    }

    static void UpdateFuelPrices()
    {
        Console.Clear();
        Console.WriteLine("--- Fiyat Güncelleme ---");

        Console.WriteLine("Yakıt tipini seçiniz D, B, L");
        char yakitFiyatGuncelle = Convert.ToChar(Console.ReadLine());

        try
        {
            double yeniFiyat = 0;

            switch (yakitFiyatGuncelle)
            {
                case 'd':
                case 'D':
                    Console.WriteLine($"Dizel : {dizel} TL/litre");
                    Console.Write("Dizel için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    dizel = yeniFiyat;
                    Console.Clear();
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"Dizel: {dizel} TL/litre ");
                    break;
                case 'b':
                case 'B':
                    Console.WriteLine($"Benzin : {benzin} TL/litre");
                    Console.Write("Benzin için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    benzin = yeniFiyat;
                    Console.Clear();
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"Benzin: {benzin} TL/litre ");
                    break;
                case 'l':
                case 'L':
                    Console.WriteLine($"LPG : {lpg} TL/litre");
                    Console.Write("LPG için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    lpg = yeniFiyat;
                    Console.Clear();
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"LPG: {lpg} TL/litre ");
                    break;
                default:
                    Console.WriteLine("Geçersiz yakıt tipi seçimi.");
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
        }
    }

    static void PerformFuelSale()
    {
        Console.Clear();
        Console.WriteLine("--- Yakıt Satışı Yap ---");

        Console.WriteLine("Yakıt tipini seçiniz D, B, L");
        char yakitSatisTipi = Convert.ToChar(Console.ReadLine());

        try
        {
            

            switch (yakitSatisTipi)
            {
                case 'd':
                case 'D':
                    
                    
                    if (dizeltank == 0)
                    {
                        Console.WriteLine("Yakıt tankında hiç dizel yakıt kalmamıştır.");
                        
                    }
                    else
                    {
                        Console.Write("Ne kadarlık dizel yakıt alışverişi yapacaksınız: ");
                        saleAmount = Convert.ToDouble(Console.ReadLine());
                        if (dizeltank < saleAmount)
                        {
                            Console.WriteLine($"Yakıt tankında {dizeltank} litre dizel kaldı. İşlem yapılamadı!");
                            // $ işareti sayesinde istenilen değerleri içine yazdık  
                        }
                        else
                        {
                            Console.WriteLine("Yakıt dolumu tamamlanmıştır.");
                            Console.WriteLine($"Yakıt tankında {dizeltank - saleAmount} litre dizel yakıt kalmıştır");
                            dizeltank = dizeltank - saleAmount;
                        }
                    }
                    break;
                case 'b':
                case 'B':
                    
                    
                    if (benzintank == 0)
                    {
                        Console.WriteLine("Yakıt tankında hiç benzin kalmamıştır.");

                    }
                    else
                    {
                        Console.Write("Ne kadarlık benzin alışverişi yapacaksınız: ");
                        saleAmount = Convert.ToDouble(Console.ReadLine());
                        if (benzintank < saleAmount)
                        {
                            Console.WriteLine($"Yakıt tankında {benzintank} litre benzin kaldı. İşlem yapılamadı!");
                            // $ işareti sayesinde istenilen değerleri içine yazdık  
                        }
                        else
                        {
                            Console.WriteLine("Yakıt dolumu tamamlanmıştır.");
                            Console.WriteLine($"Yakıt tankında {benzintank - saleAmount} litre benzin kalmıştır");
                            benzintank = benzintank - saleAmount;
                        }
                    }
                    break;
                case 'l':
                case 'L':
                    
                    
                    if (lpgtank == 0)
                    {
                        Console.WriteLine("Yakıt tankında hiç lpg kalmamıştır.");

                    }
                    else
                    {
                        Console.Write("Ne kadarlık lpg alışverişi yapacaksınız: ");
                        saleAmount = Convert.ToDouble(Console.ReadLine());
                        if (lpgtank < saleAmount)
                        {
                            Console.WriteLine($"Yakıt tankında {lpgtank} litre lpg kaldı. İşlem yapılamadı!");
                            // $ işareti sayesinde istenilen değerleri içine yazdık  
                        }
                        else
                        {
                            Console.WriteLine("Yakıt dolumu tamamlanmıştır.");
                            Console.WriteLine($"Yakıt tankında {lpgtank - saleAmount} litre lpg kalmıştır");
                            lpgtank = lpgtank - saleAmount;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Geçersiz yakıt tipi seçimi.");
                    return;
            }

            Console.WriteLine("Ödeme yöntemini seçiniz: N - Nakit, K - Kredi Kartı");
            char odemeYontemi = Convert.ToChar(Console.ReadLine());

            if (odemeYontemi == 'N' || odemeYontemi == 'n')
            {
                if (yakitSatisTipi == 'd' || yakitSatisTipi == 'D')
                    Console.WriteLine($"Nakit ödeme onaylandı. Toplam ödeme miktarı: {dizel * saleAmount}");
                // Nakit ödeme işlemleri burada gerçekleştirilebilir.
                else if (yakitSatisTipi == 'b' || yakitSatisTipi == 'B')
                    Console.WriteLine($"Nakit ödeme onaylandı. Toplam ödeme miktarı: {benzin * saleAmount}");
                else if (yakitSatisTipi == 'l' || yakitSatisTipi == 'L')
                    Console.WriteLine($"Nakit ödeme onaylandı. Toplam ödeme miktarı: {lpg * saleAmount}");
                Console.ReadKey();//sonda fiyatı gösterdikten sonra beklemesi için
                Console.Clear();
            }
            else if (odemeYontemi == 'K' || odemeYontemi == 'k')
            {
                try
                {
                    // Kredi kartı bilgilerini al
                    Console.Write("Kredi kartı numarasını giriniz: ");
                    string kartNumarasi = Console.ReadLine();
                    string kartNumarasiFormatli = FormatKartNumarasi(kartNumarasi);

                    static string FormatKartNumarasi(string kartNumarasi)
                    {
                        // Girilen kredi kartı numarasındaki boşlukları temizle
                        kartNumarasi = kartNumarasi.Replace(" ", "");

                        // Boşluk ekleyerek formatlı hale getir
                        string formatliKartNumarasi = string.Empty;
                        for (int i = 0; i < kartNumarasi.Length; i++)
                        {
                            // Her dört karakterde bir boşluk ekle
                            if (i > 0 && i % 4 == 0)
                            {
                                formatliKartNumarasi += " ";
                            }
                            formatliKartNumarasi += kartNumarasi[i];
                        }

                        return formatliKartNumarasi;
                    }

                    Console.Write("Son kullanma tarihini (MM/YY) giriniz: ");
                    string sonKullanmaTarihi = Console.ReadLine();

                    Console.Write("CVV kodunu giriniz: ");
                    string cvv = Console.ReadLine();

                    // Kredi kartı bilgilerini kontrol eden metod

                    // Kredi kartı numarasının 16 karakter uzunluğunda olması kontrolü
                    if (kartNumarasi.Length != 16)
                    {
                        throw new Exception("Geçersiz kredi kartı numarası uzunluğu.");
                    }

                    // Son kullanma tarihi formatının MM/YY olması kontrolü
                    if (!Regex.IsMatch(sonKullanmaTarihi, @"^\d{2}/\d{2}$"))
                    {
                        throw new Exception("Geçersiz son kullanma tarihi formatı. (MM/YY)");
                    }
                    /* Nasıl yapabilirim diye internetten araştırdığımda buldum
                     * ^ dizi başlagıcı temsil eder
                     * \\d{2} iki rakamın olmsı gerktiği ve arasında / olmsı gerekir
                     * $ sonu temsil eder
                     * @ işareti bir "raw string" ifadesi oluşturduğunu belirtir. Bu, gerçekleşen kaçış dizilerini (escape characters) önlemek için kullanılır
                     */

                    // CVV kodunun 3 karakter uzunluğunda olması kontrolü
                    if (cvv.Length != 3)
                    {
                        throw new Exception("Geçersiz CVV kodu uzunluğu.");
                    }

                    Console.Clear();
                    Console.WriteLine("Kredi Kart Numarası: " + kartNumarasiFormatli + " olan kredi kartından ödeme yapılmıştır.");
                    if (yakitSatisTipi == 'd' || yakitSatisTipi == 'D')
                        Console.WriteLine($"Toplam ödenen miktarı: {dizel * saleAmount} TL");
                    else if (yakitSatisTipi == 'b' || yakitSatisTipi == 'B')
                        Console.WriteLine($"Toplam ödenen miktarı: {benzin * saleAmount} TL");
                    else if (yakitSatisTipi == 'l' || yakitSatisTipi == 'L')
                        Console.WriteLine($"Toplam ödenen miktarı: {lpg * saleAmount} TL");
                    Console.ReadKey();//sonda fiyatı gösterdikten sonra beklemesi için
                    Console.Clear();

                }
                catch
                {
                    Console.WriteLine("Bir hata oluştu: ");
                }

            }


        }
        catch
        {
            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
        }
    } 

    static void ShowTankStatus()
    {
        Console.Clear();
        Console.WriteLine("--- Depo Durumunu Göster ---");
        ShowTankPercentage("Dizel", dizeltank, 12);
        ShowTankPercentage("Benzin", benzintank, 10);
        ShowTankPercentage("LPG", lpgtank, 9);
    }

    static void ShowTankPercentage(string fuelType, double tankCapacity, int factor)
    {
        Console.WriteLine($"{fuelType} yakıt tankı %{(tankCapacity / factor)} doludur.");
        Console.ReadKey();
        Console.Clear();
    }

    static void ShowTotalSales()
    {
        Console.Clear();
        Console.WriteLine("--- Toplam Satışı Göster ---");
        ShowTotalSale("Dizel", dizel, dizeltank, 1200);
        ShowTotalSale("Benzin", benzin, benzintank, 1000);
        ShowTotalSale("LPG", lpg, lpgtank, 900);
    }

    static void ShowTotalSale(string fuelType, double unitPrice, double tankCapacity, int totalCapacity)
    {
        double soldAmount = totalCapacity - tankCapacity;
        double totalAmount = soldAmount * unitPrice;

        Console.WriteLine($"Satılan toplam {fuelType} yakıt: {soldAmount} litre");
        Console.WriteLine($"{fuelType} yakıt tutarı: {totalAmount} TL");

        Console.ReadKey();
        Console.Clear();
    }


    static void UserMenu()
    {
        Console.Clear();
        Console.WriteLine("Kullanıcı girişi başarılı!");
        Console.ReadKey();
        Console.Clear();

        int anaMenu = 0;

        do
        {
            Console.WriteLine("Yakıt Satış Takip Uygulaması\n");

            Console.WriteLine("1- Yakıt Fiyatları");
            Console.WriteLine("2- Yakıt Alışı Yap");
            Console.WriteLine("3- Yakıt deposunun doluluk oranını göster");

            Console.WriteLine("0- Çıkış");

            Console.Write("Seçiminiz: ");
            anaMenu = Convert.ToInt32(Console.ReadLine());

            switch (anaMenu)
            {
                case 1:
                    ShowFuelPrices();
                    break;
                case 2:
                    PerformFuelSale();
                    break;
                case 3:
                    ShowTankStatus();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }

        } while (anaMenu != 9); //döngü 9 değeri gelene kadar sonsuz olarak dönmesini sağlar

    }

}

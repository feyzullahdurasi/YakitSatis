using System;

class Program
{
    static string adminUsername = "admin";
    static string adminPassword = "admin123";
    static string userUsername = "user";
    static string userPassword = "user123";

    static double dizel = 40, benzin = 38, lpg = 17;
    static double dizeltank = 1200, benzintank = 1000, lpgtank = 900;

    static void Main()
    {
        Console.WriteLine("Yakıt Satış Takip");

    // Kullanıcı ve yönetici girişi
    LoginMenu:
        Console.Write("Kullanıcı adınızı giriniz: ");
        string username = Console.ReadLine();
        Console.Write("Şifrenizi giriniz: ");
        string password = Console.ReadLine();

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
            goto LoginMenu;
        }
    }

    static bool IsAdmin(string username, string password)
    {
        return username == adminUsername && password == adminPassword;
    }

    static bool IsUser(string username, string password)
    {
        return username == userUsername && password == userPassword;
    }

    static void AdminMenu()
    {
        Console.WriteLine("Admin girişi başarılı!");

        int anaMenu = 0;

        do
        {
            Console.WriteLine("Yakıt Satış Takip");
            Console.WriteLine("1- Yakıt Fiyatları");
            Console.WriteLine("2- Fiyat Güncelleme");
            Console.WriteLine("3- Yakıt Satışı Yap");
            Console.WriteLine("4- Yakıt deposunun doluluk oranını göster");
            Console.WriteLine("5- Toplam Satışı Göster");
            Console.WriteLine("9- Çıkış");

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
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }

        } while (anaMenu != 9);
    }

    static void ShowFuelPrices()
    {
        Console.Clear();
        Console.WriteLine("--- Yakıt Fiyatları ---");
        Console.WriteLine($"Dizel : {dizel} ₺/litre");
        Console.WriteLine($"Benzin : {benzin} ₺/litre");
        Console.WriteLine($"LPG : {lpg} ₺/litre");

        Console.WriteLine("1- Ana Menüye dön\n2- Çıkış");
        int altMenu = Convert.ToInt32(Console.ReadLine());

        if (altMenu == 1)
        {
            Console.Clear();
        }
        else if (altMenu == 2)
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
                    Console.WriteLine($"Dizel : {dizel} ₺/litre");
                    Console.Write("Dizel için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    dizel = yeniFiyat;
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"Dizel: {dizel} ₺/litre ");
                    break;
                case 'b':
                case 'B':
                    Console.WriteLine($"Benzin : {benzin} ₺/litre");
                    Console.Write("Benzin için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    benzin = yeniFiyat;
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"Benzin: {benzin} ₺/litre ");
                    break;
                case 'l':
                case 'L':
                    Console.WriteLine($"LPG : {lpg} ₺/litre");
                    Console.Write("LPG için yeni fiyat giriniz: ");
                    yeniFiyat = Convert.ToDouble(Console.ReadLine());
                    lpg = yeniFiyat;
                    Console.WriteLine("Değişiklik yapılmıştır.");
                    Console.WriteLine($"LPG: {lpg} ₺/litre ");
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
            double satışMiktari = 0;

            switch (yakitSatisTipi)
            {
                case 'd':
                case 'D':
                    SaleOperation(dizeltank, "Dizel", dizel, out satışMiktari);
                    dizeltank -= satışMiktari;
                    break;
                case 'b':
                case 'B':
                    SaleOperation(benzintank, "Benzin", benzin, out satışMiktari);
                    benzintank -= satışMiktari;
                    break;
                case 'l':
                case 'L':
                    SaleOperation(lpgtank, "LPG", lpg, out satışMiktari);
                    lpgtank -= satışMiktari;
                    break;
                default:
                    Console.WriteLine("Geçersiz yakıt tipi seçimi.");
                    return;
            }

            // Kredi kartı bilgilerini al
            Console.Write("Kredi kartı numarasını giriniz: ");
            string kartNumarasi = Console.ReadLine();

            Console.Write("Son kullanma tarihini (MM/YY) giriniz: ");
            string sonKullanmaTarihi = Console.ReadLine();

            Console.Write("CVV kodunu giriniz: ");
            string cvv = Console.ReadLine();

            // Kredi kartı bilgilerini işle (simülasyon)
            ProcessCreditCard(kartNumarasi, sonKullanmaTarihi, cvv, satışMiktari);
        }
        catch
        {
            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
        }
    }

    static void ProcessCreditCard(string cardNumber, string expirationDate, string cvv, double amount)
    {
        // Burada kredi kartı bilgilerini güvenli bir şekilde işleyen bir kod olmalıdır.
        // Gerçek uygulamalarda ödeme işlemleri için ödeme işleme sağlayıcıları (payment gateway) kullanılmalıdır.
        Console.WriteLine($"Kredi kartı ödeme işlemi başarılı. Toplam tutar: {amount} ₺");
    }

    static void SaleOperation(double tankCapacity, string fuelType, double unitPrice, out double saleAmount)
    {
        if (tankCapacity == 0)
        {
            Console.WriteLine($"Yakıt tankında hiç {fuelType} kalmamıştır.");
            saleAmount = 0;
        }
        else
        {
            Console.Write($"Ne kadarlık {fuelType} alışverişi yapacaksınız: ");
            saleAmount = Convert.ToDouble(Console.ReadLine());
            if (tankCapacity < saleAmount)
            {
                Console.WriteLine($"Yakıt tankında {tankCapacity} litre {fuelType} kaldı. İşlem yapılamadı!");
            }
            else
            {
                Console.WriteLine("Yakıt dolumu tamamlanmıştır.");
                Console.WriteLine($"Yakıt tankında {tankCapacity - saleAmount} litre {fuelType} kalmıştır");
            }
        }
    }

    static void ShowTankStatus()
    {
        Console.Clear();
        Console.WriteLine("--- Depo Durumu Göster ---");
        ShowTankPercentage("Dizel", dizeltank, 12);
        ShowTankPercentage("Benzin", benzintank, 10);
        ShowTankPercentage("LPG", lpgtank, 9);
    }

    static void ShowTankPercentage(string fuelType, double tankCapacity, int factor)
    {
        Console.WriteLine($"{fuelType} yakıt tankı %{(tankCapacity / factor)} doludur.");
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
        Console.WriteLine($"{fuelType} yakıt tutarı: {totalAmount} ₺");
    }




    static void UserMenu()
    {
        Console.WriteLine("Kullanıcı girişi başarılı!");


        int anaMenu = 0;

        do
        {
            Console.WriteLine("Yakıt Satış Takip");
            Console.WriteLine("1- Yakıt Fiyatları");
            Console.WriteLine("2- Yakıt Alışı Yap");
            Console.WriteLine("3- Yakıt deposunun doluluk oranını göster");

            Console.WriteLine("9- Çıkış");

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

                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }

        } while (anaMenu != 9);


        static void ShowFuelPrices()
        {
            Console.Clear();
            Console.WriteLine("--- Yakıt Fiyatları ---");
            Console.WriteLine($"Dizel : {dizel} ₺/litre");
            Console.WriteLine($"Benzin : {benzin} ₺/litre");
            Console.WriteLine($"LPG : {lpg} ₺/litre");

            Console.WriteLine("1- Ana Menüye dön\n2- Çıkış");
            int altMenu = Convert.ToInt32(Console.ReadLine());

            if (altMenu == 1)
            {
                Console.Clear();
            }
            else if (altMenu == 2)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Geçersiz seçim. Ana menüye dönülüyor.");
                Console.Clear();
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
                double satışMiktari = 0;

                switch (yakitSatisTipi)
                {
                    case 'd':
                    case 'D':
                        SaleOperation(dizeltank, "Dizel", dizel, out satışMiktari);
                        dizeltank -= satışMiktari;
                        break;
                    case 'b':
                    case 'B':
                        SaleOperation(benzintank, "Benzin", benzin, out satışMiktari);
                        benzintank -= satışMiktari;
                        break;
                    case 'l':
                    case 'L':
                        SaleOperation(lpgtank, "LPG", lpg, out satışMiktari);
                        lpgtank -= satışMiktari;
                        break;
                    default:
                        Console.WriteLine("Geçersiz yakıt tipi seçimi.");
                        return;
                }

                // Kredi kartı bilgilerini al
                Console.Write("Kredi kartı numarasını giriniz: ");
                string kartNumarasi = Console.ReadLine();

                Console.Write("Son kullanma tarihini (MM/YY) giriniz: ");
                string sonKullanmaTarihi = Console.ReadLine();

                Console.Write("CVV kodunu giriniz: ");
                string cvv = Console.ReadLine();

                // Kredi kartı bilgilerini işle (simülasyon)
                ProcessCreditCard(kartNumarasi, sonKullanmaTarihi, cvv, satışMiktari);
            }
            catch
            {
                Console.WriteLine("Lütfen geçerli bir değer giriniz.");
            }
        }

        static void ProcessCreditCard(string cardNumber, string expirationDate, string cvv, double amount)
        {
            // Burada kredi kartı bilgilerini güvenli bir şekilde işleyen bir kod olmalıdır.
            // Gerçek uygulamalarda ödeme işlemleri için ödeme işleme sağlayıcıları (payment gateway) kullanılmalıdır.
            Console.WriteLine($"Kredi kartı ödeme işlemi başarılı. Toplam tutar: {amount} ₺");
        }

        static void SaleOperation(double tankCapacity, string fuelType, double unitPrice, out double saleAmount)
        {
            if (tankCapacity == 0)
            {
                Console.WriteLine($"Yakıt tankında hiç {fuelType} kalmamıştır.");
                saleAmount = 0;
            }
            else
            {
                Console.Write($"Ne kadarlık {fuelType} alışverişi yapacaksınız: ");
                saleAmount = Convert.ToDouble(Console.ReadLine());
                if (tankCapacity < saleAmount)
                {
                    Console.WriteLine($"Yakıt tankında {tankCapacity} litre {fuelType} kaldı. İşlem yapılamadı!");
                }
                else
                {
                    Console.WriteLine("Yakıt dolumu tamamlanmıştır.");
                    Console.WriteLine($"Yakıt tankında {tankCapacity - saleAmount} litre {fuelType} kalmıştır");
                }
            }
        }

        static void ShowTankStatus()
        {
            Console.Clear();
            Console.WriteLine("--- Depo Durumu Göster ---");
            ShowTankPercentage("Dizel", dizeltank, 12);
            ShowTankPercentage("Benzin", benzintank, 10);
            ShowTankPercentage("LPG", lpgtank, 9);
        }

        static void ShowTankPercentage(string fuelType, double tankCapacity, int factor)
        {
            Console.WriteLine($"{fuelType} yakıt tankı %{(tankCapacity / factor)} doludur.");
        }

    }

}

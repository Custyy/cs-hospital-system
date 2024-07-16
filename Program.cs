using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

class Program {
  static string kullaniciDosyaYolu = "kullanicilar.txt";
  static string hekimDosyaYolu = "hekimler.txt";
  static string hastaDosyaYolu = "hastalar.txt";
  static string muayeneDosyaYolu = "muayeneler.txt";

  static void Main(string[] args) {
    GirisMenu();
  }

  static void GirisMenu() {
    while (true) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("=");
      Console.WriteLine("= Sisteme hoş geldiniz!");
      Console.Write("= [/]: Lütfen Kullanıcı Adınızı Giriniz: ");
      string kullaniciAdi = Console.ReadLine();
      Console.Write("= [/]: Lütfen Şifrenizi Giriniz: ");
      string sifre = Console.ReadLine();
      Console.WriteLine("=");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");

      if (KullaniciDogrula(kullaniciAdi, sifre)) {
        AnaMenu(kullaniciAdi);
        break;
      } else {
        Console.WriteLine("[!]: Kullanıcı adınızı veya şifrenizi hatalı girdiniz. Lütfen tekrar deneyiniz.");
      }
    }
  }

  static bool KullaniciDogrula(string kullaniciAdi, string sifre) {
    string sifreliSifre = MD5Sifrele(sifre);
    string[] satirlar = File.ReadAllLines(kullaniciDosyaYolu);
    foreach(string satir in satirlar) {
      string[] bilgiler = satir.Split(',');
      string gercekSifreSifrele = MD5Sifrele(bilgiler[1]);

      if (bilgiler[0] == kullaniciAdi && gercekSifreSifrele == sifreliSifre) {
        return true; // Doğrulama başarılı
      }
    }
    return false; // Doğrulama başarısız
  }

  static string MD5Sifrele(string metin) {
    using(MD5 md5 = MD5.Create()) {
      byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(metin));
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < hashBytes.Length; i++) {
        sb.Append(hashBytes[i].ToString("x2"));
      }
      return sb.ToString();
    }
  }

  static void AnaMenu(string username) {
    while (true) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [⁂]: Giriş başarılı! =");
      Console.WriteLine("= [→]: Sisteme hoş geldiniz, " + username + "!");
      Console.WriteLine("=");
      Console.WriteLine("============================");
      Console.WriteLine("= (1): Hekim İşlemleri     =");
      Console.WriteLine("= (2): Hasta İşlemleri     =");
      Console.WriteLine("= (3): Muayene İşlemleri   =");
      Console.WriteLine("= (4): Sistemden Çıkış Yap =");
      Console.WriteLine("============================");
      Console.WriteLine("= Lütfen seçim yapmak istediğiniz işlemin başında bulunan rakamı yazınız. =");
      string secim = Console.ReadLine();

      switch (secim) {
      case "1":
        HekimMenu(username);
        break;
      case "2":
        HastaMenu(username);
        break;
      case "3":
        MuayeneMenu(username);
        break;
      case "4":
        SistemdenCikis(username);
        return;
      default:
        Console.WriteLine("Geçersiz seçim!");
        break;
      }
    }
  }

  static void HekimMenu(string username) {
    while (true) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("===       HEKİM MENÜSÜ       ===");
      Console.WriteLine("=");
      Console.WriteLine("= (1): Hekim Ekle              =");
      Console.WriteLine("= (2): Hekim Sil               =");
      Console.WriteLine("= (3): Hekim Güncelle          =");
      Console.WriteLine("= (4): Tüm Hekimleri Sil       =");
      Console.WriteLine("= (5): Hekim Bul               =");
      Console.WriteLine("= (6): Hekimleri Sırala        =");
      Console.WriteLine("= (7): Hekimleri Listele       =");
      Console.WriteLine("= (8): Ana Menüye Dön          =");
      Console.WriteLine("= (9): Sistemden Çıkış Yap     =");
      Console.WriteLine("=");
      Console.WriteLine("= Lütfen seçim yapmak istediğiniz işlemin başında bulunan rakamı yazınız. =");
      string secim = Console.ReadLine();

      switch (secim) {
      case "1":
        HekimEkle(username);
        break;
      case "2":
        HekimSil(username);
        break;
      case "3":
        HekimGuncelle(username);
        break;
      case "4":
        TumHekimleriSil(username);
        break;
      case "5":
        HekimBul(username);
        break;
      case "6":
        HekimleriSirala(username);
        break;
      case "7":
        HekimleriListele(username);
        break;
      case "8":
        AnaMenu(username);
        break;
      case "9":
        SistemdenCikis(username);
        return;
      default:
        Console.WriteLine("Geçersiz seçim!");
        break;
      }
    }
  }

  static void HekimEkle(string username) {
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===   HEKİM EKLEME MENÜSÜ    ===");
    Console.WriteLine("=");

    // Hekim ekleme işlemi...
    Console.Write("= [?]: Hekim Sicil Numarası: ");
    string sicilNo = Console.ReadLine();
    Console.Write("= [?]: Hekim TC Numarası: ");
    // TC numarası 11 haneli olmalıdır.
    string tcNo = Console.ReadLine();
    while (tcNo.Length != 11) {
      Console.WriteLine("= [!]: TC numarası 11 haneli olmalıdır. Lütfen tekrar giriniz.");
      Console.Write("= [?]: Hekim TC Numarası: ");
      tcNo = Console.ReadLine();
    }
    Console.Write("= [?]: Hekim Adı: ");
    string ad = Console.ReadLine();
    Console.Write("= [?]: Hekim Soyad: ");
    string soyad = Console.ReadLine();
    Console.Write("= [?]: Hekim Cinsiyet: ");
    string cinsiyet = Console.ReadLine();
    Console.Write("= [?]: Hekim Telefon Numarası: ");
    string tel = Console.ReadLine();
    Console.Write("= [?]: Hekim Doğum Tarihi: ");
    string dogumTarih = Console.ReadLine();
    Console.Write("= [?]: Hekim İkametgâh Adresi: ");
    string adres = Console.ReadLine();

    string hekimBilgisi = sicilNo + "," + tcNo + "," + ad + "," + soyad + "," + cinsiyet + "," + tel + "," + dogumTarih + "," + adres;
    using(StreamWriter yazici = new StreamWriter(hekimDosyaYolu, true)) {
      yazici.WriteLine(hekimBilgisi);
    }
    Console.WriteLine("= [*]: Hekim başarıyla sisteme eklendi! Hekim menüsüne dönüş yapılıyor...");
    Console.WriteLine("=");
    Console.WriteLine("===   HEKİM EKLEME MENÜSÜ    ===");
    Console.WriteLine("=== HEKİM SİSTEMİ ===");

    Thread.Sleep(3000); // 3 saniye bekleyin
    // AnaMenu(username);
  }

  static void HekimSil(string username) {
    // Hekim silme işlemi...
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===    HEKİM SİLME MENÜSÜ    ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Silinecek Hekim Sicil Numarası: ");
    string sicilNo = Console.ReadLine();

    List < string > hekimler = new List < string > (File.ReadAllLines(hekimDosyaYolu));
    for (int i = 0; i < hekimler.Count; i++) {
      string[] hekimBilgileri = hekimler[i].Split(',');
      if (hekimBilgileri[0] == sicilNo) {
        hekimler.RemoveAt(i);
        break;
      }
    }

    // Hekim bulunamadıysa
    if (!hekimler.Exists(x => x.StartsWith(sicilNo))) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("===    HEKİM SİLME MENÜSÜ    ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen sicil numarası ile eşleşen bir hekim bulunamadı! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HEKİM SİLME MENÜSÜ    ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {

      File.WriteAllLines(hekimDosyaYolu, hekimler.ToArray());

      Console.WriteLine("= [*]: Hekim başarıyla sistem üzerinden silindi! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HEKİM SİLME MENÜSÜ    ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");

      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void HekimGuncelle(string username) {
    // Hekim güncelleme işlemi...
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===  HEKİM GÜNCELLEME MENÜSÜ ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Güncellenecek Hekim Sicil Numarası: ");
    string sicilNo = Console.ReadLine();

    List < string > hekimler = new List < string > (File.ReadAllLines(hekimDosyaYolu));
    for (int i = 0; i < hekimler.Count; i++) {
      string[] hekimBilgileri = hekimler[i].Split(',');
      if (hekimBilgileri[0] == sicilNo) {
        Console.WriteLine("= [?]: Hekim Adı: ");
        string ad = Console.ReadLine();
        Console.WriteLine("= [?]: Hekim Soyad: ");
        string soyad = Console.ReadLine();
        Console.WriteLine("= [?]: Hekim Cinsiyet: ");
        string cinsiyet = Console.ReadLine();
        Console.WriteLine("= [?]: Hekim Telefon Numarası: ");
        string tel = Console.ReadLine();
        Console.WriteLine("= [?]: Hekim Doğum Tarihi: ");
        string dogumTarih = Console.ReadLine();
        Console.WriteLine("= [?]: Hekim İkametgâh Adresi: ");
        string adres = Console.ReadLine();

        hekimler[i] = sicilNo + "," + hekimBilgileri[1] + "," + ad + "," + soyad + "," + cinsiyet + "," + tel + "," + dogumTarih + "," + adres;
        break;
      }
    }
    if (!hekimler.Exists(x => x.StartsWith(sicilNo))) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("===  HEKİM GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen sicil numarası ile eşleşen bir hekim bulunamadı! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===  HEKİM GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {

      File.WriteAllLines(hekimDosyaYolu, hekimler.ToArray());

      Console.WriteLine("= [*]: Hekim başarıyla güncellendi! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HEKİM SİLME MENÜSÜ    ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");

      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void TumHekimleriSil(string username) {
    // Tüm hekimleri silme işlemi...
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("=== TÜM HEKİMLERİ SİLME MENÜSÜ ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Tüm hekimler silinecek! Devam etmek istediğinize emin misiniz? (E/H)");
    string cevap = Console.ReadLine();

    if (cevap.ToUpper() == "E") {
      Console.Clear();
      File.WriteAllText(hekimDosyaYolu, string.Empty);
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("=== TÜM HEKİMLERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm hekimler başarıyla sistem üzerinden silindi! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM HEKİMLERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    } else {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("=== TÜM HEKİMLERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm hekimler silme işlemi iptal edildi! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM HEKİMLERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void HekimBul(string username) {
    // Hekim bulma işlemi...

    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===    HEKİM BULMA MENÜSÜ    ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Aranacak Hekim Sicil Numarası: ");
    string sicilNo = Console.ReadLine();

    Console.WriteLine("= [!]: Aranıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    List < string > hekimler = new List < string > (File.ReadAllLines(hekimDosyaYolu));
    foreach(string hekim in hekimler) {
      string[] hekimBilgileri = hekim.Split(',');
      if (hekimBilgileri[0] == sicilNo) {
        Console.Clear();
        Console.WriteLine("=== HEKİM SİSTEMİ ===");
        Console.WriteLine("===    HEKİM BULMA MENÜSÜ    ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: " + sicilNo + " Sicil Numarasına Sahip Hekime Ait Bilgiler :[!]");
        Console.WriteLine("= [?]: Sicil Numarası: " + hekimBilgileri[0]);
        Console.WriteLine("= [?]: TC Numarası: " + hekimBilgileri[1]);
        Console.WriteLine("= [?]: Adı: " + hekimBilgileri[2]);
        Console.WriteLine("= [?]: Soyadı: " + hekimBilgileri[3]);
        Console.WriteLine("= [?]: Cinsiyet: " + hekimBilgileri[4]);
        Console.WriteLine("= [?]: Telefon Numarası: " + hekimBilgileri[5]);
        Console.WriteLine("= [?]: Doğum Tarihi: " + hekimBilgileri[6]);
        Console.WriteLine("= [?]: İkametgâh Adresi: " + hekimBilgileri[7]);
        Console.WriteLine("=");
        Console.WriteLine("===    HEKİM BULMA MENÜSÜ    ===");
        Console.WriteLine("=== HEKİM SİSTEMİ ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: Hekim menüsüne dönüş yapmak için 1'e basınız.");
        string secim = Console.ReadLine();
        if (secim == "1") {
          HekimMenu(username);
          break;
        } else {
          break;
        }
        break;
      }
    }

    // Hekim bulunamadıysa
    if (!hekimler.Exists(x => x.StartsWith(sicilNo))) {
      Console.Clear();
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Console.WriteLine("===    HEKİM BULMA MENÜSÜ    ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen sicil numarası ile eşleşen bir hekim bulunamadı! Hekim menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HEKİM BULMA MENÜSÜ    ===");
      Console.WriteLine("=== HEKİM SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    }
  }

  static void HekimleriSirala(string username) {
    // Hekimleri sicil numarasına göre sıralama işlemi...
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===   HEKİM SIRALAMA MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hekimler doğum tarihine numarasına göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hekimler doğum tarihine numarasına göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hekimler doğum tarihine numarasına göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Hekimleri yaşına göre sıralama işlemi...
    List < string > hekimler = new List < string > (File.ReadAllLines(hekimDosyaYolu));
    hekimler.Sort((x, y) => x.Split(',')[6].CompareTo(y.Split(',')[6]));
    Console.WriteLine("=");

    for (int i = 0; i < hekimler.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] hekimBilgileri = hekimler[i].Split(',');
      Console.WriteLine("= [?]: Sicil Numarası   : " + hekimBilgileri[0]);
      Console.WriteLine("= [?]: TC Numarası      : " + hekimBilgileri[1]);
      Console.WriteLine("= [?]: Adı              : " + hekimBilgileri[2]);
      Console.WriteLine("= [?]: Soyadı           : " + hekimBilgileri[3]);
      Console.WriteLine("= [?]: Cinsiyet         : " + hekimBilgileri[4]);
      Console.WriteLine("= [?]: Telefon Numarası : " + hekimBilgileri[5]);
      Console.WriteLine("= [?]: Doğum Tarihi     : " + hekimBilgileri[6]);
      Console.WriteLine("= [?]: İkametgâh Adresi : " + hekimBilgileri[7]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===   HEKİM SIRALAMA MENÜSÜ  ===");
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hekim menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      HekimMenu(username);
    } else {
      return;
    }
  }

  static void HekimleriListele(string username) {
    // Hekimleri listeleme işlemi...
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===  HEKİM LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hekimler cinsiyete göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hekimler cinsiyete göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hekimler cinsiyete göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Hekimleri yaşına göre sıralama işlemi...
    List < string > hekimler = new List < string > (File.ReadAllLines(hekimDosyaYolu));
    hekimler.Sort((x, y) => x.Split(',')[4].CompareTo(y.Split(',')[4]));
    Console.WriteLine("=");

    for (int i = 0; i < hekimler.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] hekimBilgileri = hekimler[i].Split(',');
      Console.WriteLine("= [?]: Sicil Numarası   : " + hekimBilgileri[0]);
      Console.WriteLine("= [?]: TC Numarası      : " + hekimBilgileri[1]);
      Console.WriteLine("= [?]: Adı              : " + hekimBilgileri[2]);
      Console.WriteLine("= [?]: Soyadı           : " + hekimBilgileri[3]);
      Console.WriteLine("= [?]: Cinsiyet         : " + hekimBilgileri[4]);
      Console.WriteLine("= [?]: Telefon Numarası : " + hekimBilgileri[5]);
      Console.WriteLine("= [?]: Doğum Tarihi     : " + hekimBilgileri[6]);
      Console.WriteLine("= [?]: İkametgâh Adresi : " + hekimBilgileri[7]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===  HEKİM LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hekim menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      HekimMenu(username);
    } else {
      return;
    }

  }

  static void HastaMenu(string username) {
    // Hasta işlemleri menüsü ve işlemleri burada
    while (true) {
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===       HASTA MENÜSÜ       ===");
      Console.WriteLine("=");
      Console.WriteLine("= (1): Hasta Ekle              =");
      Console.WriteLine("= (2): Hasta Sil               =");
      Console.WriteLine("= (3): Hasta Güncelle          =");
      Console.WriteLine("= (4): Tüm Hastaları Sil       =");
      Console.WriteLine("= (5): Hasta Bul               =");
      Console.WriteLine("= (6): Hastaları Sırala        =");
      Console.WriteLine("= (7): Hastaları Listele       =");
      Console.WriteLine("= (8): Ana Menüye Dön          =");
      Console.WriteLine("= (9): Sistemden Çıkış Yap     =");
      Console.WriteLine("=");
      Console.WriteLine("= Lütfen seçim yapmak istediğiniz işlemin başında bulunan rakamı yazınız. =");
      string secim = Console.ReadLine();

      switch (secim) {
      case "1":
        HastaEkle(username);
        break;
      case "2":
        HastaSil(username);
        break;
      case "3":
        HastaGuncelle(username);
        break;
      case "4":
        TumHastalariSil(username);
        break;
      case "5":
        HastaBul(username);
        break;
      case "6":
        HastalariSirala(username);
        break;
      case "7":
        HastalariListele(username);
        break;
      case "8":
        AnaMenu(username);
        break;
      case "9":
        SistemdenCikis(username);
        return;
      default:
        Console.WriteLine("Geçersiz seçim!");
        break;
      }
    }
  }

  static void HastaEkle(string username) {
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===   HASTA EKLEME MENÜSÜ    ===");
    Console.WriteLine("=");

    // Hasta ekleme işlemi...
    Console.Write("= [?]: Hasta Adı: ");
    string ad = Console.ReadLine();
    Console.Write("= [?]: Hasta Soyad: ");
    string soyad = Console.ReadLine();
    Console.Write("= [?]: Hasta Adres: ");
    string adres = Console.ReadLine();
    Console.Write("= [?]: Hasta TC Numarası: ");
    string tcNo = Console.ReadLine();
    Console.Write("= [?]: Hasta Doğum Tarihi: ");
    string dogumTarih = Console.ReadLine();
    Console.Write("= [?]: Hasta Cinsiyet: ");
    string cinsiyet = Console.ReadLine();
    Console.Write("= [?]: Hasta Telefon Numarası: ");
    string tel = Console.ReadLine();
    Console.Write("= [?]: Hasta Kan Grubu: ");
    string kanGrubu = Console.ReadLine();
    Console.Write("= [?]: Hasta Sosyal Güvence Türü: ");
    string sosyalGuvenceTur = Console.ReadLine();

    string hastaBilgisi = ad + "," + soyad + "," + adres + "," + tcNo + "," + dogumTarih + "," + cinsiyet + "," + tel + "," + kanGrubu + "," + sosyalGuvenceTur;
    using(StreamWriter yazici = new StreamWriter(hastaDosyaYolu, true)) {
      yazici.WriteLine(hastaBilgisi);
    }
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===   HASTA EKLEME MENÜSÜ    ===");
    Console.WriteLine("=");
    Console.WriteLine("= [*]: Hasta başarıyla sisteme eklendi! Hasta menüsüne dönüş yapılıyor...");
    Console.WriteLine("=");
    Console.WriteLine("===   HASTA EKLEME MENÜSÜ    ===");
    Console.WriteLine("=== HASTA SİSTEMİ ===");

    Thread.Sleep(3000); // 3 saniye bekleyin
    // AnaMenu(username);
  }

  static void HastaSil(string username) {
    // Hasta silme işlemi...
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===    HASTA SİLME MENÜSÜ    ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Silinecek Hasta TC Numarası: ");
    string tcNo = Console.ReadLine();

    List < string > hastalar = new List < string > (File.ReadAllLines(hastaDosyaYolu));

    bool hastaBulundu = false;
    for (int i = 0; i < hastalar.Count; i++) {
      string[] hastaBilgileri = hastalar[i].Split(',');
      if (hastaBilgileri[3] == tcNo) {
        hastaBulundu = true;
        hastalar.RemoveAt(i);
        break;
      }
    }

    // Hasta bulunamadıysa
    if (!hastaBulundu) {
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===    HASTA SİLME MENÜSÜ    ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen TC numarası ile eşleşen bir hasta bulunamadı! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HASTA SİLME MENÜSÜ    ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {

      File.WriteAllLines(hastaDosyaYolu, hastalar.ToArray());
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===    HASTA SİLME MENÜSÜ    ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Hasta başarıyla sistem üzerinden silindi! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===    HASTA SİLME MENÜSÜ    ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");

      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void HastaGuncelle(string username) {
    // Hasta güncelleme işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ ===");
    Console.WriteLine("===  HASTA GÜNCELLEME MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Güncellenecek Hasta TC Numarası: ");
    string tcNo = Console.ReadLine();

    List < string > hastalar = new List < string > (File.ReadAllLines(hastaDosyaYolu));
    bool hastaBulundu = false;
    for (int i = 0; i < hastalar.Count; i++) {
      string[] hastaBilgileri = hastalar[i].Split(',');
      if (hastaBilgileri[3] == tcNo) {
        hastaBulundu = true;
        Console.Write("= [?]: Hasta Adı: ");
        string ad = Console.ReadLine();
        Console.Write("= [?]: Hasta Soyad: ");
        string soyad = Console.ReadLine();
        Console.Write("= [?]: Hasta Adres: ");
        string adres = Console.ReadLine();
        Console.Write("= [?]: Hasta Doğum Tarihi: ");
        string dogumTarih = Console.ReadLine();
        Console.Write("= [?]: Hasta Cinsiyet: ");
        string cinsiyet = Console.ReadLine();
        Console.Write("= [?]: Hasta Telefon Numarası: ");
        string tel = Console.ReadLine();
        Console.Write("= [?]: Hasta Kan Grubu: ");
        string kanGrubu = Console.ReadLine();
        Console.Write("= [?]: Hasta Sosyal Güvence Türü: ");
        string sosyalGuvenceTur = Console.ReadLine();

        hastalar[i] = ad + "," + soyad + "," + adres + "," + tcNo + "," + dogumTarih + "," + cinsiyet + "," + tel + "," + kanGrubu + "," + sosyalGuvenceTur;
        break;
      }
    }

    if (!hastaBulundu) {
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ ===");
      Console.WriteLine("===  HASTA GÜNCELLEME MENÜSÜ  ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen TC numarası ile eşleşen bir hasta bulunamadı! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===  HASTA GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {
      File.WriteAllLines(hastaDosyaYolu, hastalar.ToArray());
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ ===");
      Console.WriteLine("===  HASTA GÜNCELLEME MENÜSÜ  ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Hasta başarıyla güncellendi! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===  HASTA GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void TumHastalariSil(string username) {
    // Tüm hastaları silme işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=== TÜM HASTALARI SİLME MENÜSÜ ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Tüm hastalar silinecek! Devam etmek istediğinize emin misiniz? (E/H)");
    string cevap = Console.ReadLine();

    if (cevap.ToUpper() == "E") {
      Console.Clear();
      File.WriteAllText(hastaDosyaYolu, string.Empty);
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Console.WriteLine("=== TÜM HASTALARI SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm hastalar başarıyla sistem üzerinden silindi! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM HASTALARI SİLME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ  ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    } else {
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Console.WriteLine("=== TÜM HASTALARI SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm hastalar silme işlemi iptal edildi! Hasta menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM HASTALARI SİLME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ  ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void HastaBul(string username) {
    // Hasta bulma işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===    HASTA BULMA MENÜSÜ      ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Aranacak Hasta TC Numarası: ");
    string tcNo = Console.ReadLine();

    Console.WriteLine("= [!]: Aranıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    List < string > hastalar = new List < string > (File.ReadAllLines(hastaDosyaYolu));
    foreach(string hasta in hastalar) {
      string[] hastaBilgileri = hasta.Split(',');
      if (hastaBilgileri[3] == tcNo) {
        Console.Clear();
        Console.WriteLine("===  HASTA SİSTEMİ  ===");
        Console.WriteLine("===    HASTA BULMA MENÜSÜ      ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: " + tcNo + " TC Numarasına Sahip Hastaya Ait Bilgiler :[!]");
        Console.WriteLine("= [?]: Adı                 : " + hastaBilgileri[0]);
        Console.WriteLine("= [?]: Soyadı              : " + hastaBilgileri[1]);
        Console.WriteLine("= [?]: Adres               : " + hastaBilgileri[2]);
        Console.WriteLine("= [?]: TC Numarası         : " + hastaBilgileri[3]);
        Console.WriteLine("= [?]: Doğum Tarihi        : " + hastaBilgileri[4]);
        Console.WriteLine("= [?]: Cinsiyet            : " + hastaBilgileri[5]);
        Console.WriteLine("= [?]: Telefon Numarası    : " + hastaBilgileri[6]);
        Console.WriteLine("= [?]: Kan Grubu           : " + hastaBilgileri[7]);
        Console.WriteLine("= [?]: Sosyal Güvence Türü : " + hastaBilgileri[8]);
        Console.WriteLine("=");
        Console.WriteLine("===    HASTA BULMA MENÜSÜ      ===");
        Console.WriteLine("===  HASTA SİSTEMİ  ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: Hasta menüsüne dönüş yapmak için 1'e basınız.");
        string secim = Console.ReadLine();
        if (secim == "1") {
          HastaMenu(username);
          break;
        } else {
          break;
        }
        break;
      }
    }
  }

  static void HastalariSirala(string username) {
    // Hastaları sıralama işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===    HASTA SIRALAMA MENÜSÜ   ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hastalar doğum tarihine numarasına göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hastalar doğum tarihine numarasına göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hastalar doğum tarihine numarasına göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Hastaları yaşına göre sıralama işlemi...
    List < string > hastalar = new List < string > (File.ReadAllLines(hastaDosyaYolu));
    hastalar.Sort((x, y) => x.Split(',')[4].CompareTo(y.Split(',')[4]));
    Console.WriteLine("=");

    for (int i = 0; i < hastalar.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] hastaBilgileri = hastalar[i].Split(',');
      Console.WriteLine("= [?]: Adı                 : " + hastaBilgileri[0]);
      Console.WriteLine("= [?]: Soyadı              : " + hastaBilgileri[1]);
      Console.WriteLine("= [?]: Adres               : " + hastaBilgileri[2]);
      Console.WriteLine("= [?]: TC Numarası         : " + hastaBilgileri[3]);
      Console.WriteLine("= [?]: Doğum Tarihi        : " + hastaBilgileri[4]);
      Console.WriteLine("= [?]: Cinsiyet            : " + hastaBilgileri[5]);
      Console.WriteLine("= [?]: Telefon Numarası    : " + hastaBilgileri[6]);
      Console.WriteLine("= [?]: Kan Grubu           : " + hastaBilgileri[7]);
      Console.WriteLine("= [?]: Sosyal Güvence Türü : " + hastaBilgileri[8]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===    HASTA SIRALAMA MENÜSÜ   ===");
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hasta menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      HastaMenu(username);
    } else {
      return;
    }
  }

  static void HastalariListele(string username) {
    // Hastaları listeleme işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===    HASTA LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hastalar sosyal güvencesine göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hastalar sosyal güvencesine göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Hastalar sosyal güvencesine göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Hastaları ssk türüne göre sıralama işlemi...
    List < string > hastalar = new List < string > (File.ReadAllLines(hastaDosyaYolu));
    hastalar.Sort((x, y) => x.Split(',')[8].CompareTo(y.Split(',')[8]));
    Console.WriteLine("=");

    for (int i = 0; i < hastalar.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] hastaBilgileri = hastalar[i].Split(',');
      Console.WriteLine("= [?]: Adı                 : " + hastaBilgileri[0]);
      Console.WriteLine("= [?]: Soyadı              : " + hastaBilgileri[1]);
      Console.WriteLine("= [?]: Adres               : " + hastaBilgileri[2]);
      Console.WriteLine("= [?]: TC Numarası         : " + hastaBilgileri[3]);
      Console.WriteLine("= [?]: Doğum Tarihi        : " + hastaBilgileri[4]);
      Console.WriteLine("= [?]: Cinsiyet            : " + hastaBilgileri[5]);
      Console.WriteLine("= [?]: Telefon Numarası    : " + hastaBilgileri[6]);
      Console.WriteLine("= [?]: Kan Grubu           : " + hastaBilgileri[7]);
      Console.WriteLine("= [?]: Sosyal Güvence Türü : " + hastaBilgileri[8]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===    HASTA LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Hasta menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      HastaMenu(username);
    } else {
      return;
    }
  }

  static void MuayeneMenu(string username) {
    while (true) {
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===      MUAYENE MENÜSÜ      ===");
      Console.WriteLine("=");
      Console.WriteLine("= (1): Muayene Ekle            =");
      Console.WriteLine("= (2): Muayene Sil             =");
      Console.WriteLine("= (3): Muayene Güncelle        =");
      Console.WriteLine("= (4): Tüm Muayeneleri Sil     =");
      Console.WriteLine("= (5): Muayene Bul             =");
      Console.WriteLine("= (6): Muayeneleri Sırala      =");
      Console.WriteLine("= (7): Muayeneleri Listele     =");
      Console.WriteLine("= (8): Ana Menüye Dön          =");
      Console.WriteLine("= (9): Sistemden Çıkış Yap     =");
      Console.WriteLine("=");
      Console.WriteLine("= Lütfen seçim yapmak istediğiniz işlemin başında bulunan rakamı yazınız. =");
      string secim = Console.ReadLine();

      switch (secim) {
      case "1":
        MuayeneEkle(username);
        break;
      case "2":
        MuayeneSil(username);
        break;
      case "3":
        MuayeneGuncelle(username);
        break;
      case "4":
        TumMuayeneleriSil(username);
        break;
      case "5":
        MuayeneBul(username);
        break;
      case "6":
        MuayeneleriSirala(username);
        break;
      case "7":
        MuayeneleriListele(username);
        break;
      case "8":
        AnaMenu(username);
        break;
      case "9":
        SistemdenCikis(username);
        return;
      default:
        Console.WriteLine("Geçersiz seçim!");
        break;
      }
    }
  }

  static void MuayeneEkle(string username) {
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===   MUAYENE EKLEME MENÜSÜ  ===");
    Console.WriteLine("=");

    Console.Write("= [?]: Hasta TC Numarası: ");
    string tcNo = Console.ReadLine();
    Console.Write("= [?]: Hekim Sicil No: ");
    string sicilNo = Console.ReadLine();
    Console.Write("= [?]: Hastanın Şikayeti: ");
    string sikayet = Console.ReadLine();
    Console.Write("= [?]: Muayene Tarihi: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
    string muayeneTarih = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

    string muayeneBilgisi = muayeneTarih + "," + sikayet + "," + sicilNo + "," + tcNo;
    using(StreamWriter yazici = new StreamWriter(muayeneDosyaYolu, true)) {
      yazici.WriteLine(muayeneBilgisi);
    }
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===   MUAYENE EKLEME MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [*]: Muayene başarıyla sisteme eklendi! Muayene menüsüne dönüş yapılıyor...");
    Console.WriteLine("=");
    Console.WriteLine("===   MUAYENE EKLEME MENÜSÜ  ===");
    Console.WriteLine("=== HASTA SİSTEMİ ===");

    Thread.Sleep(3000); // 3 saniye bekleyin
    // AnaMenu(username);
  }

  static void MuayeneSil(string username) {
    // Muayene silme işlemi...
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===   MUAYENE SİLME MENÜSÜ   ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Silinecek Muayene Tarihi: ");
    string muayeneTarih = Console.ReadLine();

    List < string > muayeneler = new List < string > (File.ReadAllLines(muayeneDosyaYolu));

    bool muayeneBulundu = false;
    for (int i = 0; i < muayeneler.Count; i++) {
      string[] muayeneBilgileri = muayeneler[i].Split(',');
      if (muayeneBilgileri[0] == muayeneTarih) {
        muayeneBulundu = true;
        muayeneler.RemoveAt(i);
        break;
      }
    }

    // Muayene bulunamadıysa
    if (!muayeneBulundu) {
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===   MUAYENE SİLME MENÜSÜ   ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen muayene tarihi ile eşleşen bir muayene bulunamadı! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===   MUAYENE SİLME MENÜSÜ   ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {

      File.WriteAllLines(muayeneDosyaYolu, muayeneler.ToArray());
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===   MUAYENE SİLME MENÜSÜ   ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Muayene başarıyla sistem üzerinden silindi! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===   MUAYENE SİLME MENÜSÜ   ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
    }

    // AnaMenu(username);
  }

  static void MuayeneGuncelle(string username) {
    // Muayene güncelleme işlemi...
    Console.Clear();
    Console.WriteLine("=== HASTA SİSTEMİ ===");
    Console.WriteLine("===  MUAYENE GÜNCELLEME MENÜSÜ ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Güncellenecek Muayene Tarihi: ");
    string muayeneTarih = Console.ReadLine();

    List < string > muayeneler = new List < string > (File.ReadAllLines(muayeneDosyaYolu));
    bool muayeneBulundu = false;
    for (int i = 0; i < muayeneler.Count; i++) {
      string[] muayeneBilgileri = muayeneler[i].Split(',');
      if (muayeneBilgileri[0] == muayeneTarih) {
        muayeneBulundu = true;
        Console.Write("= [?]: Hasta TC Numarası: ");
        string tcNo = Console.ReadLine();
        Console.Write("= [?]: Hekim Sicil No: ");
        string sicilNo = Console.ReadLine();
        Console.Write("= [?]: Hastanın Şikayeti: ");
        string sikayet = Console.ReadLine();

        muayeneler[i] = muayeneTarih + "," + sikayet + "," + sicilNo + "," + tcNo;
        break;
      }
    }

    if (!muayeneBulundu) {
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ ===");
      Console.WriteLine("=== MUAYENE GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen muayene tarihi ile eşleşen bir muayene bulunamadı! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===  MUAYENE GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    } else {
      File.WriteAllLines(muayeneDosyaYolu, muayeneler.ToArray());
      Console.Clear();
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Console.WriteLine("===  MUAYENE GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Muayene başarıyla güncellendi! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== MUAYENE GÜNCELLEME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void TumMuayeneleriSil(string username) {
    // Tüm muayeneleri silme işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=== TÜM MUAYENELERİ SİLME MENÜSÜ ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Tüm muayeneler silinecek! Devam etmek istediğinize emin misiniz? (E/H)");
    string cevap = Console.ReadLine();

    if (cevap.ToUpper() == "E") {
      Console.Clear();
      File.WriteAllText(muayeneDosyaYolu, string.Empty);
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Console.WriteLine("=== TÜM MUAYENELERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm muayeneler başarıyla sistem üzerinden silindi! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM MUAYENELERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ  ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    } else {
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Console.WriteLine("=== TÜM MUAYENELERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=");
      Console.WriteLine("= [*]: Tüm muayeneler silme işlemi iptal edildi! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("=== TÜM MUAYENELERİ SİLME MENÜSÜ ===");
      Console.WriteLine("=== HASTA SİSTEMİ  ===");
      Thread.Sleep(3000); // 3 saniye bekleyin
      // AnaMenu(username);
    }
  }

  static void MuayeneBul(string username) {
    // Muayene bulma işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===    MUAYENE BULMA MENÜSÜ      ===");
    Console.WriteLine("=");
    Console.Write("= [?]: Aranacak Muayene Tarihi: ");
    string muayeneTarih = Console.ReadLine();

    Console.WriteLine("= [!]: Aranıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Aranıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    List < string > muayeneler = new List < string > (File.ReadAllLines(muayeneDosyaYolu));
    bool muayeneBulundu = false;
    foreach(string muayene in muayeneler) {
      string[] muayeneBilgileri = muayene.Split(',');
      if (muayeneBilgileri[0] == muayeneTarih) {
        Console.Clear();
        Console.WriteLine("===  HASTA SİSTEMİ  ===");
        Console.WriteLine("===   MUAYENE BULMA MENÜSÜ     ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: " + muayeneTarih + " Tarihine Sahip Muayeneye Ait Bilgiler :[!]");
        Console.WriteLine("= [?]: Muayene Tarihi        : " + muayeneBilgileri[0]);
        Console.WriteLine("= [?]: Hastanın Şikayeti     : " + muayeneBilgileri[1]);
        Console.WriteLine("= [?]: Hekim Sicil No        : " + muayeneBilgileri[2]);
        Console.WriteLine("= [?]: Hasta TC Numarası     : " + muayeneBilgileri[3]);
        Console.WriteLine("=");
        Console.WriteLine("===   MUAYENE BULMA MENÜSÜ     ===");
        Console.WriteLine("===  HASTA SİSTEMİ  ===");
        Console.WriteLine("=");
        Console.WriteLine("= [!]: Muayene menüsüne dönüş yapmak için 1'e basınız.");
        string secim = Console.ReadLine();
        if (secim == "1") {
          MuayeneMenu(username);
          break;
        } else {
          break;
        }
        break;
      }
    }

    // AnaMenu(username);
    // Eğer muayene bulunamazsa
    if (!muayeneBulundu) {
      Console.Clear();
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Console.WriteLine("===   MUAYENE BULMA MENÜSÜ     ===");
      Console.WriteLine("=");
      Console.WriteLine("= [!]: Girilen muayene tarihi ile eşleşen bir muayene bulunamadı! Muayene menüsüne dönüş yapılıyor...");
      Console.WriteLine("=");
      Console.WriteLine("===   MUAYENE BULMA MENÜSÜ     ===");
      Console.WriteLine("===  HASTA SİSTEMİ  ===");
      Thread.Sleep(5000); // 5 saniye bekleyin
      // AnaMenu(username)
    }
  }

  static void MuayeneleriSirala(string username) {
    // Muayeneleri sıralama işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===   MUAYENE SIRALAMA MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Muayeneler tarihine göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Muayeneler tarihine göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Muayeneler tarihine göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Muayeneleri tarihe göre sıralama işlemi...
    List < string > muayeneler = new List < string > (File.ReadAllLines(muayeneDosyaYolu));
    muayeneler.Sort((x, y) => x.Split(',')[0].CompareTo(y.Split(',')[0]));
    Console.WriteLine("=");

    for (int i = 0; i < muayeneler.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] muayeneBilgileri = muayeneler[i].Split(',');
      Console.WriteLine("= [?]: Muayene Tarihi        : " + muayeneBilgileri[0]);
      Console.WriteLine("= [?]: Hastanın Şikayeti     : " + muayeneBilgileri[1]);
      Console.WriteLine("= [?]: Hekim Sicil No        : " + muayeneBilgileri[2]);
      Console.WriteLine("= [?]: Hasta TC Numarası     : " + muayeneBilgileri[3]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===   MUAYENE SIRALAMA MENÜSÜ  ===");
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Muayene menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      MuayeneMenu(username);
    } else {
      return;
    }
  }

  static void MuayeneleriListele(string username) {
    // Muayeneleri listeleme işlemi...
    Console.Clear();
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("===   MUAYENE LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Muayeneler hekimlere göre sıralanıyor.");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Muayeneler hekimlere göre sıralanıyor..");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= [!]: Muayeneler hekimlere göre sıralanıyor...");
    Thread.Sleep(1000); // 1 saniye bekleyin

    // Muayeneleri tarihe göre sıralama işlemi...
    List < string > muayeneler = new List < string > (File.ReadAllLines(muayeneDosyaYolu));
    muayeneler.Sort((x, y) => x.Split(',')[2].CompareTo(y.Split(',')[2]));
    Console.WriteLine("=");

    for (int i = 0; i < muayeneler.Count; i++) {
      Thread.Sleep(1000); // 1 saniye bekleyin
      string[] muayeneBilgileri = muayeneler[i].Split(',');
      Console.WriteLine("= [?]: Muayene Tarihi        : " + muayeneBilgileri[0]);
      Console.WriteLine("= [?]: Hastanın Şikayeti     : " + muayeneBilgileri[1]);
      Console.WriteLine("= [?]: Hekim Sicil No        : " + muayeneBilgileri[2]);
      Console.WriteLine("= [?]: Hasta TC Numarası     : " + muayeneBilgileri[3]);
      Console.WriteLine("=");
    }

    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("=");
    Console.WriteLine("===   MUAYENE LİSTELEME MENÜSÜ  ===");
    Console.WriteLine("===  HASTA SİSTEMİ  ===");
    Console.WriteLine("=");
    Console.WriteLine("= [!]: Muayene menüsüne dönüş yapmak için 1'e basınız.");
    string secim = Console.ReadLine();
    if (secim == "1") {
      MuayeneMenu(username);
    } else {
      return;
    }
  }

  static void SistemdenCikis(string username) {
    Console.Clear();
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Console.WriteLine("===       HEKİM MENÜSÜ       ===");
    Console.WriteLine("=");
    Console.WriteLine("= Sistemden çıkış yapılıyor.   =");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= Sistemden çıkış yapılıyor..  =");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= Sistemden çıkış yapılıyor... =");
    Thread.Sleep(1000); // 1 saniye bekleyin
    Console.WriteLine("= Sağlıklı günler dileriz, " + username + "!");
    Console.WriteLine("=");
    Console.WriteLine("===       HEKİM MENÜSÜ       ===");
    Console.WriteLine("=== HEKİM SİSTEMİ ===");
    Thread.Sleep(3000); // 1 saniye bekleyin

    // Sistemden çıkış işlemi
    System.Environment.Exit(0);
  }
}
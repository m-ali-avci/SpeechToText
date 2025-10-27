# SpeechToTextApp (C# WinForms)

Bu proje, C# (.NET Framework 4.8) ve System.Speech.Recognition kütüphanesi kullanılarak geliştirilmiş, konuşmayı metne dönüştüren (Speech to Text) bir Windows Forms uygulamasıdır.  
Amaç, mikrofon üzerinden alınan sesi gerçek zamanlı olarak metin alanına yazdırmaktır.

---

## Özellikler
- Mikrofon üzerinden ses alma  
- Konuşmayı anlık olarak metne dönüştürme  
- Türkçe veya sistem dili destekli çalışma  
- Hata durumlarında kullanıcıya bilgi verme  
- Ücretsiz ve tamamen çevrimdışı çalışır (Windows’un yerel konuşma motorunu kullanır)

---

## Kullanılan Teknolojiler
- C#
- .NET Framework 4.8
- Windows Forms (WinForms)
- System.Speech.Recognition kütüphanesi

---

## Gereksinimler
1. Windows 10 veya 11 işletim sistemi  
2. .NET Framework 4.8 yüklü olmalı  
   - İndirme bağlantısı: https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48  
3. Mikrofon bağlantısı ve izinleri açık olmalı  
   - Ayarlar → Gizlilik → Mikrofon → “Uygulamaların mikrofon erişimine izin ver” aktif olmalı  
4. Türkçe tanıma kullanılacaksa:  
   - Ayarlar → Zaman ve Dil → Dil ve Bölge → Türkçe (Seçenekler) → Konuşma dili paketini indir  

---

## Kurulum ve Çalıştırma

### Seçenek 1: Hazır EXE ile
1. `release/` klasöründeki `SpeechToTextApp.exe` dosyasını çalıştırın.  
2. “Dinlemeyi Başlat” butonuna tıklayın.  
3. Mikrofon izni penceresi çıkarsa izin verin ve konuşun.  

### Seçenek 2: Visual Studio 2022 ile
1. `SpeechToTextApp.sln` dosyasını Visual Studio 2022 ile açın.  
2. Proje ayarlarında hedef framework olarak **.NET Framework 4.8** seçildiğinden emin olun.  
3. Menüden **Build → Rebuild Solution** seçin.  
4. Programı **F5** tuşuna basarak çalıştırın.  

---

## Kullanım
1. Program açıldığında metin kutusunda “Hazır” mesajı görünecektir.  
2. “Dinlemeyi Başlat” butonuna tıklayın.  
3. Konuşmaya başlayın.  
4. Tanınan metin alttaki metin kutusunda görüntülenecektir.  
5. “Durdur” butonuna tıklayarak dinlemeyi sonlandırabilirsiniz.  

---

## Olası Hata Durumları ve Çözümler
- **“Speech to text başlatılamadı”**:  
  - Türkçe konuşma paketi yüklü değilse, Windows dil ayarlarından konuşma paketini ekleyin.  
  - Mikrofon erişimi açık olduğundan emin olun.  
  - .NET Framework 4.8’in kurulu olduğuna emin olun.  

- **“Motor hazır değil”**:  
  - Kodda `SpeechRecognitionEngine` tanımlanmadıysa veya dil paketi yüklenmediyse oluşabilir.  
  - Bilgisayarı yeniden başlatıp tekrar deneyin.  

---

## Geliştirici Notları
Bu proje bir dönemlik ödev olarak hazırlanmıştır.  
Türkçe konuşma tanıma doğruluğu, Windows konuşma paketi ve ortam gürültüsüne bağlı olarak değişebilir.

---

## Lisans
Bu proje yalnızca eğitim amaçlı hazırlanmıştır ve ticari kullanım için tasarlanmamıştır.

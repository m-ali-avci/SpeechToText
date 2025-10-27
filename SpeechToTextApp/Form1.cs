using System;
using System.Linq;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace SpeechToTextApp
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine _engine;
        private bool _listening = false;

        public Form1()
        {
            InitializeComponent();

            // Olay bağlamaları
            this.Load += Form1_Load;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Log("=== Sistem Tanılama Başlıyor ===");

               
                var list = SpeechRecognitionEngine.InstalledRecognizers().ToList();
                if (list.Count == 0)
                {
                    Log("❌ Hiç konuşma tanıyıcısı bulunamadı. Windows konuşma dili paketleri eksik.");
                    Log("Ayarlar → Zaman ve Dil → Dil ve Bölge → Türkçe (Seçenekler) → Konuşma dilini indir.");
                    return;
                }

                Log($"Bulunan tanıyıcı sayısı: {list.Count}");
                foreach (var ri in list)
                {
                    Log($"- {ri.Name} | Culture={ri.Culture} | Description={ri.Description}");
                }

                
                var tr = list.FirstOrDefault(r => r.Culture.Name.Equals("tr-TR", StringComparison.OrdinalIgnoreCase));
                if (tr != null)
                {
                    Log("tr-TR tanıyıcı bulundu. Onunla başlatılıyor...");
                    _engine = new SpeechRecognitionEngine(tr);
                }
                else
                {
                    Log("⚠️ tr-TR yok. Varsayılan tanıyıcı ile başlatılıyor...");
                    _engine = new SpeechRecognitionEngine(); // default recognizer
                }

                
                try
                {
                    _engine.LoadGrammar(new DictationGrammar());
                    Log("Gramer yüklendi (DictationGrammar).");
                }
                catch (Exception ex)
                {
                    Log("❌ Gramer yükleme hatası: " + ex.Message);
                }

               
                try
                {
                    _engine.SetInputToDefaultAudioDevice();
                    Log("Mikrofon girişine bağlandı (default audio device).");
                }
                catch (InvalidOperationException ex)
                {
                    Log("❌ Mikrofon bağlama hatası (InvalidOperationException): " + ex.Message);
                    Log("Kontrol: Ayarlar → Gizlilik ve Güvenlik → Mikrofon → Uygulamalara mikrofon erişimi AÇIK olmalı.");
                    return;
                }
                catch (Exception ex)
                {
                    Log("❌ Mikrofon bağlama hatası: " + ex.Message);
                    return;
                }

                
                _engine.SpeechRecognized += Engine_SpeechRecognized;
                _engine.SpeechHypothesized += Engine_SpeechHypothesized;
                _engine.RecognizeCompleted += Engine_RecognizeCompleted;

                Log($"Hazır. Kullanılan tanıyıcı: {_engine.RecognizerInfo?.Name} ({_engine.RecognizerInfo?.Culture})");
                Log("Not: Türkçe doğruluğu için Windows konuşma dili paketinde Türkçe yüklü olmalı.");
                Log("=== Tanılama Bitti ===");
            }
            catch (Exception ex)
            {
                Log("❌ Başlatma genel hatası: " + ex.Message);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (_engine == null)
            {
                Log("❌ Motor hazır değil. Yukarıdaki tanılama loguna bak.");
                return;
            }
            if (_listening)
            {
                Log("Zaten dinliyorum.");
                return;
            }

            try
            {
                _engine.RecognizeAsync(RecognizeMode.Multiple);
                _listening = true;
                Log("🎤 Dinleme başladı. Konuşabilirsiniz…");
            }
            catch (InvalidOperationException ex)
            {
                Log("❌ Dinleme başlatma hatası (InvalidOperationException): " + ex.Message);
                Log("Genelde RecognizeAsync zaten çalışırken çağrılırsa olur. Programı durdurup yeniden dene.");
            }
            catch (Exception ex)
            {
                Log("❌ Dinleme başlatma hatası: " + ex.Message);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (_engine == null)
            {
                Log("Motor hazır değil.");
                return;
            }
            if (!_listening)
            {
                Log("Zaten durdurulmuş.");
                return;
            }

            try
            {
                _engine.RecognizeAsyncStop();
                _listening = false;
                Log("⏹️ Dinleme durduruldu.");
            }
            catch (Exception ex)
            {
                Log("❌ Durdurma hatası: " + ex.Message);
            }
        }

        private void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var text = e.Result?.Text ?? "";
            var conf = e.Result?.Confidence ?? 0;
            Log($"✅ TANINDI ({conf:P0}): {text}");
        }

        private void Engine_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Log($"~ Tahmin: {e.Result?.Text}");
        }

        private void Engine_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null) Log("Tanıma hatası: " + e.Error.Message);
            if (e.Cancelled) Log("Tanıma iptal edildi.");
        }

        private void Log(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => Log(msg)));
                return;
            }
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }

        private void txtLog_TextChanged(object sender, EventArgs e) { }
    }
}

# 🚀 Task Manager | Modern Görev Yönetim Sistemi


## 📋 Proje Hakkında

Task Manager, modern iş hayatının karmaşık görev yönetimi ihtiyaçlarını karşılamak için tasarlanmış, kullanıcı dostu bir web uygulamasıdır. Bu uygulama ile:

- 📅 Günlük, haftalık ve aylık görevlerinizi organize edebilir
- ✅ Görevlerinizin durumlarını kolayca takip edebilir
- 🔄 İş akışınızı verimli bir şekilde yönetebilir
- 📊 Görev ilerlemelerinizi görsel olarak izleyebilirsiniz

### 🎯 Neden Task Manager?

- **Kolay Kullanım**: Sezgisel arayüzü sayesinde herkes rahatlıkla kullanabilir
- **Güvenli**: JWT tabanlı kimlik doğrulama sistemi ile verileriniz güvende
- **Hızlı**: Modern teknolojiler ile geliştirilmiş performanslı altyapı
- **Responsive**: Her cihazda kusursuz çalışan adaptif tasarım

## ⚡ Özellikler

### 👤 Kullanıcı Yönetimi
- Güvenli kayıt ve giriş sistemi
- JWT tabanlı oturum yönetimi
- Şifreli veri saklama

### 📝 Görev Yönetimi
- Sürükle-bırak görev organizasyonu
- Görev önceliklendirme
- Durum takibi (Yapılacak, Devam Ediyor, Tamamlandı)
- Detaylı görev açıklamaları

### 🔍 Akıllı Filtreleme
- Tarih bazlı filtreleme
- Durum bazlı filtreleme
- Öncelik bazlı sıralama

## 🛠️ Teknoloji Stack

<div align="center">
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg" alt=".NET Core" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="C#" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" alt="SQL Server" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/html5/html5-original.svg" alt="HTML5" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/css3/css3-original.svg" alt="CSS3" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/javascript/javascript-original.svg" alt="JavaScript" width="40" height="40"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/bootstrap/bootstrap-plain.svg" alt="Bootstrap" width="40" height="40"/>
</div>

### Backend
- **.NET 7.0** - Modern ve hızlı backend framework
- **Entity Framework Core** - Güçlü ORM aracı
- **SQL Server** - Güvenilir veritabanı sistemi
- **JWT Authentication** - Güvenli kimlik doğrulama
- **Swagger/OpenAPI** - Kapsamlı API dokümantasyonu

### Frontend
- **HTML5 & CSS3** - Modern web standartları
- **JavaScript** - Dinamik kullanıcı deneyimi
- **Bootstrap 5** - Responsive tasarım framework'ü
- **Font Awesome** - Zengin ikon kütüphanesi

## 🚀 Kurulum

1. **Projeyi Klonlayın**
   \`\`\`bash
   git clone https://github.com/[kullanıcı-adınız]/task-manager.git
   cd task-manager
   \`\`\`

2. **Bağımlılıkları Yükleyin**
   \`\`\`bash
   dotnet restore
   \`\`\`

3. **Veritabanını Oluşturun**
   \`\`\`bash
   dotnet ef database update
   \`\`\`

4. **Uygulamayı Çalıştırın**
   \`\`\`bash
   dotnet run
   \`\`\`

## 📱 Ekran Görüntüleri
### Back-end kısmı Front-end kullanılarak bir arayüz eklenmemiş şekilde
<div align="center">
  <img src="Screenshots/back-end.png" alt="Back-end Ekranı" width="400"/>

</div>

### Front-end kullanılarak bir arayüz eklenmiş şekilde
<div align="center">
  <img src="screenshots/login.png" alt="Giriş Ekranı" width="400"/>
  <img src="screenshots/dashboard.png" alt="Görev Paneli" width="400"/>
  <img src="screenshots/task-create.png" alt="Görev Oluşturma" width="400"/>
  <img src="screenshots/task-list.png" alt="Görev Listesi" width="400"/>
</div>

## 🌐 API Kullanımı

### Kimlik Doğrulama
\`\`\`http
POST /api/auth/register
POST /api/auth/login
\`\`\`

### Görev İşlemleri
\`\`\`http
GET /api/tasks
GET /api/tasks/{id}
POST /api/tasks
PUT /api/tasks/{id}
DELETE /api/tasks/{id}
\`\`\`

## 🔐 Güvenlik Özellikleri

- ✅ JWT tabanlı güvenli kimlik doğrulama
- 🔒 Şifre hashleme
- 🛡️ CORS koruması
- 🔍 Input validasyonu
- 🚫 XSS ve CSRF koruması


## 📞 İletişim

- Website: [your-website.com](https://your-website.com)
- Twitter: [@twitter_handle](https://twitter.com/twitter_handle)
- LinkedIn: [linkedin_profile](https://linkedin.com/in/linkedin_profile)
- E-mail: your.email@example.com

---

<div align="center">
  ⭐️ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
</div> 

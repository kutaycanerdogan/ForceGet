Gerekli credentiallar için appsettings.json dosyası örnek alınıp appsettings.Development.json editlenebilir.

Redis yüklü değilse cache mekanizması silinebilir.
ApiNinjas key gerekli.
Ücretsiz ApiNinjas kısıtlamaları:
  - [Cities](https://api-ninjas.com/api/city) için limit ve offset premium özellikler
  - [Country](https://api-ninjas.com/api/country) limit kullanılabilir deniyor ancak sadece 1 kayıt dönmekte.
  - [Convert Currency](https://api-ninjas.com/api/convertcurrency) Majör birimlerde işlem yapılamıyor.

Bu yüzden localden countries.json ile veriler çekildi, autocomplete yapıldı (backend), debounce kullanıldı(frontend)

Basit bir login, register yapısı, login hatırlama, Authorisation, Authenticaiton yapıları, swagger, extention methodlar, codefirst yaklaşımı, 
loglama (sadece yapı kuruldu, istenen kütüphane entegrasyonu yapılabilir), auto DB Migration, Global Error handling, Jsonproperty ve enum customisation yapıldı.

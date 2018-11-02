# FromWilma2InfoTV
Datan siirto Wilmasta InfoTV:hen. Loggautuu sisään Wilmaan ja hakee esimerkissä tietyn luokkatilan (muokkaa haluamaksesi) varaukset ja muodostaa niistä poco-muotoisen tietueen jota voi sitten jatkojalostaa esim. infotv:hen näkyviin.

HUOM!

Lisää projektiin seuraavat settingsit app.configiin:
wilmaUrl -> osoite wilma palvelimeesi, https://wilma.organisaatiosi.fi/ (viimein / tärkeä!)
username -> palvelutunnus, jolla otat Json rest apiin yhteyttä.
passwd -> palvelutunnuksen salasana
companySpesificKey -> saat Vismalta omasi

<applicationSettings>
    <Testing_Primus_Json.Properties.Settings>
      <setting name="wilmaUrl" serializeAs="String">
        <value>https://wilmapalvelimesiosoite/</value>
      </setting>
      <setting name="passwd" serializeAs="String">
        <value>palvelutunnuksesisalasana</value>
      </setting>
      <setting name="username" serializeAs="String">
        <value>palvelutunnuksesi</value>
      </setting>
      <setting name="companySpesificKey" serializeAs="String">
        <value>yrityksesi_api-key_jonka_saat_wismalta</value>
      </setting>
    </Testing_Primus_Json.Properties.Settings>
  </applicationSettings>
  
  Jotta projektia voi testata.




New-SelfSignedCertificate -Subject "device1-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\3F90675A1E446B9C0710D9B32870EB3AFE75CA4A)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device1.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device1.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device1.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

New-SelfSignedCertificate -Subject "device2-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\603CE24E1D72E9F263AC465B7E51C8EE846B9B86)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device2.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device2.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device2.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

New-SelfSignedCertificate -Subject "device3-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\C513A43E52016117584D3ECD914A3CC05828BE51)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device3.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device3.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device3.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

New-SelfSignedCertificate -Subject "device4-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\0A3240F96897A843E3E55864B2B5FC4473BBC0B1)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device4.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device4.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device4.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

New-SelfSignedCertificate -Subject "device5-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\52B2CDC1DBBB848E94D82AF10050CE5B5356F304)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device5.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device5.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device5.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

New-SelfSignedCertificate -Subject "device6-tenant1" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec Signature -KeyExportPolicy Exportable
$cert = (Get-ChildItem -Path cert:\LocalMachine\My\D14441F60DB19ABB803AFF3C74C88669B858DFEB)
$pwd = Read-Host -AsSecureString
Export-PfxCertificate -Cert $cert -FilePath "D:\Documents\cert\device6.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "D:\Documents\cert\device6.cer" -Type CERT 
Set-Content -Path "D:\Documents\cert\device6.cer" -Value ([Convert]::ToBase64String($cert.RawData)) -Encoding Ascii

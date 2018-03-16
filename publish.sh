systemctl stop kestrel-tematre.me.service

dotnet restore
dotnet msbuild TemaTre.Site.Resume.sln
dotnet publish -o /home/TemaTre.Cv.Site/


nginx -s reload
systemctl start kestrel-tematre.me.service

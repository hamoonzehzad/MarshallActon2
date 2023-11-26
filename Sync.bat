setlocal
cd /d %~dp0
git pull
git add --all
git commit -m "Automatic sync operation."
git push
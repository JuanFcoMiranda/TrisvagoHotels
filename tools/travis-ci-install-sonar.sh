#!/bin/sh
echo "Starting install..."
wget -O sonar.zip https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.7.1.2311/sonar-scanner-msbuild-4.7.1.2311-netcoreapp2.0.zip
echo "Unzipping..."
unzip -qq sonar.zip -d tools/sonar
echo "Displaying file structure..."
find .
ls -l tools/sonar
echo "Changing permissions..."
chmod +x tools/sonar/sonar-scanner-4.1.0.1829/bin/sonar-scanner
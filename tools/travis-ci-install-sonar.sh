#!/bin/sh
echo "Starting install..."
wget -O sonar.zip https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/5.4.1.41282/sonar-scanner-msbuild-5.4.1.41282-net5.0.zip
echo "Unzipping..."
unzip -qq sonar.zip -d tools/sonar
echo "Changing permissions..."
chmod +x tools/sonar/sonar-scanner-4.6.1.2450/bin/sonar-scanner

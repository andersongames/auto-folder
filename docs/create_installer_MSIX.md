# 📦 Steps to create the AutoFolder.UI MSIX Installer

## 1. Add a Packaging Project

In Visual Studio:

1. Right-click the **Solution** → **Add** → **New Project....**
2. Search for *Windows Application Packaging Project*.
3. Name it, for example: *AutoFolder.Package*.
4. This project will appear in the solution as a separate project.

## 2. Configure dependency for AutoFolder.UI
In the AutoFolder.Package project:

1. Click on **Dependencies** → **Add Reference...** → select the *AutoFolder.UI* project.
This ensures that when the MSIX is generated, it will embed your executable.

## 3. Configure package properties

In Package.appxmanifest (automatically generated in AutoFolder.Package), edit:

- **DisplayName**: AutoFolder
- **Publisher**: CN=YourNameOrCompany
- **Version**: 1.0.0.0
- **Logo**: point to your icon **(Assets\Square150x150Logo.png, etc.)**
⚠️ *Here you need to provide icons in the required dimensions (44x44, 150x150, etc). Visual Studio can automatically generate them from the .ico.*

**🔹 Lines you MUST edit:**

```bash
<Identity
    Name="e7d05fdf-b011-4504-9692-a5cd7ad1c2a5"
    Publisher="CN=ander"
    Version="1.0.0.0" />
```

- **Name** → must be unique on the system. If you don't intend to publish to the Microsoft Store, you can use something like: *Name="AutoFolder.UI"*
- **Publisher** → needs to match the certificate used for signing. For testing, you can leave it as is *(CN=ander)*.
- **Version** → enter the actual app version (e.g., 1.0.0.0).

```bash
<Properties>
  <DisplayName>AutoFolder</DisplayName>
  <PublisherDisplayName>Anderson Games</PublisherDisplayName>
  <Logo>Images\StoreLogo.png</Logo>
</Properties>
```

- **DisplayName** → name displayed in the Start menu, Task Manager, etc. Here you should put *AutoFolder*.
- **PublisherDisplayName** → the "pretty" name of the publisher (does not need to be the same as the CN). Ex: *Anderson Games*.
- **Logo** → you will swap it with the icons you already have in the project *(StoreLogo.png etc)*.

```bash
<uap:VisualElements
    DisplayName="AutoFolder"
    Description="Utility to organize files automatically by common prefix"
    BackgroundColor="transparent"
    Square150x150Logo="Images\Square150x150Logo.png"
    Square44x44Logo="Images\Square44x44Logo.png">
  <uap:DefaultTile Wide310x150Logo="Images\Wide310x150Logo.png" />
  <uap:SplashScreen Image="Images\SplashScreen.png" />
</uap:VisualElements>
```

- **DisplayName** → must be the same as in `<Properties>` *(AutoFolder)*.
- **Description** → edit to a real description, for example: *"Automatically organizes files by their common prefix."*
- **Square150x150Logo / Square44x44Logo / Wide310x150Logo / SplashScreen** → replace with the correct images (Visual Studio can automatically generate multiple dimensions from your .ico).

**🔹 Lines you DO NOT need to change:**

- `<TargetDeviceFamily>` → leave as is (Windows.Desktop).
- `<Capabilities>` → runFullTrust is mandatory for packaged WinForms/WPF apps.
- `<Resources>` → x-generate is standard.

**🔹 Generate images from .png using Visual Studio:**

1. Right-click the project **AutoFolder.Package** → **Properties**.
2. Go to the **Visual Assets** tab (inside Package.appxmanifest, in designer mode).
3. There you can select your *.png*.
4. Visual Studio automatically generates all the images (StoreLogo.png, Square150x150Logo.png, SplashScreen.png etc.) in the required dimensions.

⚠️ *If the **Visual Assets** tab does not appear, open **Package.appxmanifest** in the **designer**, not the XML editor.*

### 4. Package signing

Windows only installs MSIX if it is **signed**:
- For testing, Visual Studio generates a developer certificate (.pfx).
- You can install this certificate on your machine with a **double-click** → "**Install certificate**".

To distribute publicly:

- You need a valid certificate from a Certificate Authority (e.g., Digicert, Sectigo).

### 5. Publish the installer

In Solution Explorer:

1. Right-click the project **AutoFolder.Package** → **Publish** → **Create App Packages....**
2. Choose "**Sideloading**" (if not for the Microsoft Store).
3. Visual Studio generates the AppPackages folder with:
   - The .msix (AutoFolder installer).
   - The .cer certificate to install.
   - A PowerShell script for installation (Add-AppDevPackage.ps1).
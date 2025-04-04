$images = @{
    "iphone13.jpg" = "https://images.pexels.com/photos/1334597/pexels-photo-1334597.jpeg"
    "macbook.jpg" = "https://images.pexels.com/photos/18105/pexels-photo.jpg"
    "ipad.jpg" = "https://images.pexels.com/photos/1334598/pexels-photo-1334598.jpeg"
    "samsung.jpg" = "https://images.pexels.com/photos/404280/pexels-photo-404280.jpeg"
    "dell.jpg" = "https://images.pexels.com/photos/7974/pexels-photo.jpg"
    "airpods.jpg" = "https://images.pexels.com/photos/3780681/pexels-photo-3780681.jpeg"
    "watch.jpg" = "https://images.pexels.com/photos/437037/pexels-photo-437037.jpeg"
    "surface.jpg" = "https://images.pexels.com/photos/7974/pexels-photo.jpg"
    "sony.jpg" = "https://images.pexels.com/photos/3394650/pexels-photo-3394650.jpeg"
    "lg.jpg" = "https://images.pexels.com/photos/6976094/pexels-photo-6976094.jpeg"
}

$imagePath = "wwwroot/images"
if (!(Test-Path $imagePath)) {
    New-Item -ItemType Directory -Path $imagePath
}

foreach ($image in $images.GetEnumerator()) {
    $url = $image.Value
    $filename = $image.Key
    $filepath = Join-Path $imagePath $filename
    
    Write-Host "Téléchargement de $filename..."
    try {
        Invoke-WebRequest -Uri $url -OutFile $filepath -UseBasicParsing
        Write-Host "Téléchargé avec succès !"
    }
    catch {
        Write-Host "Erreur lors du téléchargement de $filename : $_"
    }
} 
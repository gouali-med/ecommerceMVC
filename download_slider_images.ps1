# Créer le dossier pour les images du slider s'il n'existe pas
$sliderPath = "EcommerceMVC.Web/wwwroot/images/slider"
if (-not (Test-Path $sliderPath)) {
    New-Item -ItemType Directory -Path $sliderPath -Force
}

# URLs des images du slider
$images = @{
    "tech1.jpg" = "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=1920&q=80"
    "tech2.jpg" = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=1920&q=80"
    "tech3.jpg" = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=1920&q=80"
    "tech4.jpg" = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=1920&q=80"
}

# Télécharger chaque image
foreach ($image in $images.GetEnumerator()) {
    $outputPath = Join-Path $sliderPath $image.Key
    Write-Host "Downloading $($image.Key)..."
    
    try {
        Invoke-WebRequest -Uri $image.Value -OutFile $outputPath
        Write-Host "✓ Downloaded successfully" -ForegroundColor Green
    }
    catch {
        Write-Host "✗ Error downloading: $_" -ForegroundColor Red
    }
}

Write-Host "`nDownload completed!" 
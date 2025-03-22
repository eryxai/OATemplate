# Prompt the user to enter the old and new model names
$oldWord = Read-Host "Please enter the old model name"
$newWord = Read-Host "Please enter the new model name"

# Initialize a counter to keep track of the number of renamed files
$count = 0

# Get all files in the current directory and subdirectories that contain the old word in their name
Get-ChildItem -Recurse -Filter "*$oldWord*" | ForEach-Object {
    # Create the new file name by replacing the old word with the new word
    $newName = $_.Name -replace [regex]::Escape($oldWord), $newWord
    # Rename the file
    Rename-Item -Path $_.FullName -NewName $newName
    # Increment the counter
    $count++
}

# Display the number of files that have been changed
Write-Host "$count files have been changed."




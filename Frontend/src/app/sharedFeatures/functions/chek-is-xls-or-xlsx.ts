export function checkFileExtension(fileName: string): boolean {
  const validExtensions = ['.xls', '.xlsx'];
  const fileExtension = fileName
    .substr(fileName.lastIndexOf('.'))
    .toLowerCase();
  return validExtensions.includes(fileExtension);
}

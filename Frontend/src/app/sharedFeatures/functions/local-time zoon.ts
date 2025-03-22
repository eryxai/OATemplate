export function convertToEgyptLocalDate(dateString: string | number | Date) {
  const date = new Date(dateString);
  const options = { timeZone: 'Africa/Cairo' };
  return date.toLocaleString('en-US', options);
}

/**
 *
 * @param event
 * @param keysToRename
 * @returns
 */
export function renameKeys(
  event: Record<string, any>,
  keysToRename: Record<string, string>
): Record<string, any> {
  const renamedKeysObj: Record<string, any> = {};

  Object.entries(keysToRename).forEach(([oldKey, newKey]) => {
    if (Object.prototype.hasOwnProperty.call(event, oldKey)) {
      renamedKeysObj[newKey] = event[oldKey];
      delete event[oldKey];
    }
  });

  return { ...event, ...renamedKeysObj };
}

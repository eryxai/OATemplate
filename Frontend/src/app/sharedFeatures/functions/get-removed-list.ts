export function getRemovedObjects(
  original: any[],
  listAfterRemoving: any[]
): any[] {
  const removedObjects: any[] = [];

  for (const obj of original) {
    const matchingObjIndex = listAfterRemoving.findIndex(
      item => item.id === obj.id
    );
    if (matchingObjIndex === -1) {
      removedObjects.push(obj);
    }
  }

  return removedObjects;
}

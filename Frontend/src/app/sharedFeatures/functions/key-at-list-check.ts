export function hasProperty(list: any[], property: string): boolean {
  let isFount: boolean = false;
  for (const iterator of list) {
    if (iterator.key) {
      isFount = true;
      break;
    }
  }
  return isFount;
}

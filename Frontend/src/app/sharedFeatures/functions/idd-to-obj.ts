import { DropdownItem } from '../models/Dropdown-item';

/**
 *
 * @param ids
 * @param lookup
 * @returns
 */
export function convertIdsToObj(
  ids: any[],
  lookup: DropdownItem[]
): DropdownItem[] {
  // 
  return ids
    .map(id => {
      const match = lookup.find(entry => entry.id === id);
      if (match) {
        return match;
      } else {
        return null;
      }
    })
    .filter((entry): entry is { id: number; name: string } => entry !== null);
}

import { Item, ITableRow } from '../models/itable-row';

/**
 *
 * @param items
 * @param propertyGetters
 * @returns
 */
export function groupBy(
  items: Item[],
  propertyGetters: ((item: Item) => any)[]
): ITableRow[] {
  let currentIndex = 0;

  const groups = items.reduce((acc: { [key: string]: Item[] }, item: Item) => {
    let group: any = acc;

    propertyGetters.forEach((getter, index) => {
      const propertyValue = getter(item);
      if (index === propertyGetters.length - 1) {
        group[propertyValue] = (group[propertyValue] || []).concat(item);
      } else {
        group[propertyValue] = group[propertyValue] || {};
        group = group[propertyValue];
      }
    });

    return acc;
  }, {});

  const addIndexToGroups = (groups: any, index: number): ITableRow[] => {
    return Object.keys(groups).map((key: string) => {
      const items = groups[key];
      const children = Array.isArray(items);
      const result: ITableRow = {
        key,
        items: [],
        index,
        count: children ? items.length : 1, // count of items at this level
      };

      if (children) {
        result.items = items;
      } else {
        const childGroups = addIndexToGroups(items, index + 1);
        result.items = childGroups;
        result.count = childGroups.reduce(
          (count, child) => count + (child.count ?? 0),
          0
        );
      }

      return result;
    });
  };

  const result = addIndexToGroups(groups, currentIndex);

  return result;
}

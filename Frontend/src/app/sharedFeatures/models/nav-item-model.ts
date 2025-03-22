export interface NavItemModel {
  order: number;
  link?: string;
  name: string;
  title: string;
  icon?: string;
  img?: string;
  cssClass: string;
  childs: NavItemModel[];
  data?: any;
  activeClass?: boolean;
}

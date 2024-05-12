export interface MeteoritesFiltersReq {
  fromYear: number;
  toYear: number;
  meteoriteName: string;
  meteoriteClass: string;
  sortableField: string;
  isDesc: boolean;
  take: number;
  skip: number;
}

export interface ReqGetMeteorites {
  fromYear: number;
  toYear: number;
  meteoriteName: string;
  className: string;
  sortField: string;
  take: number;
  skip: number;
  isDesc: boolean;
}

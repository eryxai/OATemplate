export interface ILocationMarker {
  id: number;
  locationName: string;
  noOfIssues: number;
  noOfSolvedIssues: number;
  readiness: string | number;
  hasChild: boolean;
  eventId: number;
  noOfVisits: number;
  coordinates: {
    lat: number;
    lng: number;
  };
  marker: string; // from code FE TS
}

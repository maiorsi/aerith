import League from './league.interface';
import Season from './season.interface';

export default interface Tournament {
  createdDate: Date;
  id: number;
  league: League;
  name: string;
  season: Season;
}

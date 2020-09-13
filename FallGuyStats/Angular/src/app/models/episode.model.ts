import { Round } from './round.model';

export class Episode {

    constructor() {
        this.Rounds = [];
    }
    public Kudos: number;
    public Fame: number;
    public Crowns: number;
    public Rounds: Round[];
}

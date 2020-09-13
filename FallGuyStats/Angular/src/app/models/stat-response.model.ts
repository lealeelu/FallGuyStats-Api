export class SessionStat {
    public crownCount: number
    public episodeCount: number
    public cheaterCount: number
    public roundsSinceCrown: number
}

export class RoundStats {
    public RoundType: string
    public GoldCount: number 
    public SilverCount: number 
    public BronzeCount: number 
    public QualifiedCount: number 
    public NotQualifiedCount: number 
}

export class StatResponse {
    public todayStats: SessionStat
    public seasonStats: SessionStat
    public roundStats: RoundStats
    public currentRound: string
}
export class SessionStat {
    public crownCount: number
    public episodeCount: number
    public cheaterCount: number
    public roundsSinceCrown: number
}

export class RoundStats {
    public roundType: string
    public goldCount: number
    public silverCount: number
    public bronzeCount: number
    public qualifiedCount: number
    public notQualifiedCount: number
}

export class StatResponse {
    public todayStats: SessionStat
    public seasonStats: SessionStat
    public roundStats: RoundStats
    public currentRound: string
}

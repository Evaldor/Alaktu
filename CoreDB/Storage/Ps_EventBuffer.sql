﻿CREATE PARTITION SCHEME [Ps_EventBuffer]
    AS PARTITION [Pf_EventBuffer]
    TO ([PRIMARY],[PRIMARY],[PRIMARY],[PRIMARY]);


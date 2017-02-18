# Performance.AutoRest
Shows how autorest is bad for performance

## Summary

### 2 Threads for 10 seconds
* AutoRest: Total Requests: 487, **Failed: 257**
* Static HTTPCLIENT: Total Requests: 486, **Failed: 0**

### 10 Threads for 10 seconds
* AutoRest: Total Requests: 3182, **Failed: 3182**
* Static HTTPCLIENT: Total Requests: 2321, **Failed: 0**

## AUTOREST
<pre>
Threads: 2, Total Time: 10,00 secs  Req/Sec: 48,70 
Total Requests: 487, Failed: 257
Avg Time: 42,36 ms, Min Time: 28,00 ms, Max Time: 87,00 ms 

Threads: 2, Total Time: 20,00 secs, Req/Sec: 56,00 
Total Requests: 1 120, Failed: 890 
Avg Time: 36,12 ms, Min Time: 28,00 ms, Max Time: 98,00 ms 

Threads: 1, Total Time: 30,00 secs, Req/Sec: 30,77
Total Requests: 923, Failed: 880 
Avg Time: 32,38 ms, Min Time: 29,00 ms, Max Time: 117,00 ms 

Threads: 10, Total Time: 10,00 secs, Req/Sec: 318,20
Total Requests: 3 182, Failed: 3 182 
Avg Time: 31,33 ms, Min Time: 28,00 ms, Max Time: 62,00 ms
</pre>

## Static HTTPCLIENT
<pre>
Threads: 2, Total Time: 10,00 secs, Req/Sec: 48,60 
Total Requests: 486, Failed: 0 
Avg Time: 44,63 ms, Min Time: 41,00 ms, Max Time: 66,00 ms 

Threads: 2, Total Time: 10,00 secs, Req/Sec: 48,80 
Total Requests: 488, Failed: 0 
Avg Time: 44,56 ms, Min Time: 41,00 ms, Max Time: 65,00 ms 

Threads: 1, Total Time: 30,00 secs, Req/Sec: 21,90 
Total Requests: 657, Failed: 0 
Avg Time: 46,09 ms, Min Time: 42,00 ms, Max Time: 78,00 ms 

Threads: 10, Total Time: 10,00 secs, Req/Sec: 232,10 
Total Requests: 2 321, Failed: 0
Avg Time: 46,73 ms, Min Time: 41,00 ms, Max Time: 96,00 ms 
</pre>

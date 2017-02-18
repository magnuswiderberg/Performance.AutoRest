# Performance.AutoRest
Shows how autorest is bad for performance

## Summary 1
Test setup with Service web calling Facade web which in turn called Api web.
All deployed on a very weak machine in Azure.

### 2 Threads for 10 seconds
* AutoRest: Total Requests: 487, **Failed: 257**
* Static HTTPCLIENT: Total Requests: 486, **Failed: 0**

### 10 Threads for 10 seconds
* AutoRest: Total Requests: 3182, **Failed: 3034**
* Static HTTPCLIENT: Total Requests: 2321, **Failed: 0**

## Summary 2
Test setup with Facade web calling Api web.
All deployed on a very weak machine in Azure.

### 2 Threads for 20 seconds
* AutoRest: Total Requests: 612, **Failed: 408**, Req/Sec: 30,60
* Static HTTPCLIENT: Total Requests: 1027, **Failed: 0**, Req/Sec: 51,35

### 10 Threads for 20 seconds
* AutoRest: Total Requests: 6468, **Failed: 6168**, Req/Sec: 323,40
* Static HTTPCLIENT: Total Requests: 5483, **Failed: 0**, Req/Sec: 274,15

## SERVICE / FACADE / API
### AUTOREST
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
Total Requests: 3 182, Failed: 3 034
Avg Time: 31,33 ms, Min Time: 28,00 ms, Max Time: 62,00 ms
</pre>

### Static HTTPCLIENT
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

## FACADE / API
### AUTOREST
<pre>
Total Requests: 612, Failed: 408
Threads: 2, Total Time: 20,00 secs, Req/Sec: 30,60
Avg Time: 66,56 ms, Min Time: 29,00 ms, Max Time: 9 838,00 ms

Total Requests: 6 468, Failed: 6 168 
Threads: 10, Total Time: 20,00 secs, Req/Sec: 323,40
Avg Time: 31,38 ms, Min Time: 27,00 ms, Max Time: 75,00 ms
</pre>

### Static HTTPCLIENT
<pre>
Total Requests: 1 027, Failed: 0 
Threads: 2, Total Time: 20,00 secs, Req/Sec: 51,35 
Avg Time: 39,52 ms, Min Time: 36,00 ms, Max Time: 77,00 ms

Total Requests: 5 483, Failed: 0
Threads: 10, Total Time: 20,00 secs, Req/Sec: 274,15
Avg Time: 37,70 ms, Min Time: 33,00 ms, Max Time: 97,00 ms
</pre>


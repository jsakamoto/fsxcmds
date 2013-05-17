fsxcmds
=======

F# scripts for command line

Each command Reference
---------

### httpd.fsx

#### summary

Start small http daemon.  
It only can respond static file contents.

#### usage / 使い方

    > httpd [-root {path}][-port {port-number}][-host {hostname}]

#### arguments / 引数
<dl>
<dt>-root {path}</dt>
<dd>Specify the directory path to publish http. Current directory is default. / http に公開するディレクトリのパス。既定値はカレントディレクトリ。</dd>

<dt>-port {pot-number}</dt>
<dd>Specify the port number of the TCP connection to listen. 8080 is default. / リッスンするTCP接続のポート番号。既定値は 8080。</dd>

<dt>-host {hostname}</dt>
<dd>Specify the host name to accept request. "localhost" is default, and you can specify "*" as a wild card. / 要求を受け付けるホスト名。既定値は "localhost" で、ワイルドカードとして "*" を指定可能。</dd>
</dl>
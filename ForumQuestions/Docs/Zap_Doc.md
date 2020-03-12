# Zap Mitigation Steps

Cookie Policy security has been added.
Also added content headers for:

X-Content-Type-Options, "nosniff"
X-Frame-Options, "sameorigin"
X-XSS-Protection, "1"
and Cache-Control, "no-cache"


Zap is still detecting issues with the cache settings, but allowing Cookie Policy is already applied in project.


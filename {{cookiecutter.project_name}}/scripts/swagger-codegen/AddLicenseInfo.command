#!/bin/sh

NEW_UUID=$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 32 | head -n 1)
echo "Adding license notice to $1"
cp ../../copyright-notice.txt "$NEW_UUID" 
cat "$1" >> "$NEW_UUID"
mv "$NEW_UUID" "$1"
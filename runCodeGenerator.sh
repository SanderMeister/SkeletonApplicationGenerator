#!/bin/bash

cd Microservices
for dir in */
do
    cd ${dir}
    dotnet build
    cd ..
done

cd backend
cd App.API
docker build -t app .
cd ..
cd Identity.API
docker build -t identity .
cd ..
cd GW.API
docker build -t gw .
cd ..
cd ..
cd frontend
docker build -t frontend .
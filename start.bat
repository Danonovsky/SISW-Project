kubectl create -f postgres-pvc.yaml
kubectl apply -f postgres-deployment.yaml,identity-deployment.yaml,app-deployment.yaml,gw-deployment.yaml,frontend-deployment.yaml
kubectl apply -f postgres-service.yaml,identity-service.yaml,app-service.yaml,gw-service.yaml,frontend-service.yaml
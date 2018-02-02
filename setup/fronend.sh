docker pull ivanpaulovich/acerola:latest-frontend
docker run -p 8001:80 -d \
	--name acerola-frontend ivanpaulovich/acerola:latest-frontend

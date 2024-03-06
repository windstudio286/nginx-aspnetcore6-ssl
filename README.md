# Hướng dẫn

## Chạy docker local
Chạy docker compose -f docker-compose_local.yml up
## Chạy docker trên server

Cấu hình let's encrypt
Mặc định lần đầu để let's encrypt tạo được private key vì let’s encrypt cần path /.well-known/acme-challenge/ để tạo ra key private nên sau khi chạy câu lệnh sẽ sinh ra tại path /var/www/certbot

Chạy docker compose up
Vào docker của nginx để reload lại config

**docker exec -it <container_id> bash**
Sau đó test nginx và reload config bằng câu lệnh
```bash
nginx -t
nginx -s reload
```

Nếu ko được thì restart lại docker nginx.proxy
**docker compose up -d nginx.proxy**
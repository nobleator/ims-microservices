"""
Interface with RabbitMQ broker to receive optimization requests and send back solutions.

See: https://dev.to/usamaashraf/microservices--rabbitmq-on-docker-e2f
"""
import pika
import time
import json


def callback(ch, method, properties, body):
    print(" [x] Received %r" % body)
    print("Decoding...")
    str_obj = body.decode("utf-8")
    print(f"Decoded object: {str_obj}")
    data = json.loads(str_obj)
    print("JSON data object:")
    print(json.dumps(data, sort_keys=True, indent=4))


def listen():
    print("Starting optimizer broker connection...")
    ready = False
    while not ready:
        try:
            params = pika.ConnectionParameters(host="broker")
            connection = pika.BlockingConnection(params)
            print("Connection ready")
            ready = True
        except pika.exceptions.AMQPConnectionError:
            print("Connection error, waiting 2s then trying again")
            time.sleep(2)

    channel = connection.channel()
    channel.queue_declare(queue="optimization_jobs")
    channel.basic_consume(queue="optimization_jobs",
                          auto_ack=True,
                          on_message_callback=callback)

    print(" [*] Waiting for messages. To exit press CTRL+C")
    channel.start_consuming()

if __name__ == "__main__":
    print("Sleeping 10s to allow other services to startup")
    time.sleep(10)
    print("Waking, attempting to connect to broker")
    listen()
#include <iostream>

enum class TrafficLight {
    RED,
    GREEN,
    YELLOW
};

TrafficLight nextLight(TrafficLight current) {
    switch(current) {
        case TrafficLight::RED: return TrafficLight::GREEN;
        case TrafficLight::GREEN: return TrafficLight::YELLOW;
        case TrafficLight::YELLOW: return TrafficLight::RED;
        default: return TrafficLight::RED;
    }
}

int main() {
    setlocale(LC_ALL, "ru");
    TrafficLight light = TrafficLight::RED;
    
    // Проедем цикл светофора
    for(int i = 0; i < 5; i++) {
        std::cout << "Текущий свет: ";
        switch(light) {
            case TrafficLight::RED: std::cout << "Красный"; break;
            case TrafficLight::GREEN: std::cout << "Зеленый"; break;
            case TrafficLight::YELLOW: std::cout << "Желтый"; break;
        }
        std::cout << std::endl;
        
        light = nextLight(light);
    }
    
    return 0;
}

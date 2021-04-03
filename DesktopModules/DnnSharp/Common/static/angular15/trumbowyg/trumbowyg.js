/**
 * Trumbowyg v2.6.0 - A lightweight WYSIWYG editor
 * Trumbowyg core file
 * ------------------------
 * @link http://alex-d.github.io/Trumbowyg
 * @license MIT
 * @author Alexandre Demode (Alex-D)
 *         Twitter : @AlexandreDemode
 *         Website : alex-d.fr
 */

jQuery.trumbowyg = {
    langs: {
        en: {
            viewHTML: 'View HTML',
            undo: 'Undo',
            redo: 'Redo',
            formatting: 'Formatting',
            p: 'Paragraph',
            blockquote: 'Quote',
            code: 'Code',
            header: 'Header',
            bold: 'Bold',
            italic: 'Italic',
            strikethrough: 'Stroke',
            underline: 'Underline',
            strong: 'Strong',
            em: 'Emphasis',
            del: 'Deleted',
            superscript: 'Superscript',
            subscript: 'Subscript',
            unorderedList: 'Unordered list',
            orderedList: 'Ordered list',
            insertImage: 'Insert Image',
            link: 'Link',
            createLink: 'Insert link',
            unlink: 'Remove link',
            justifyLeft: 'Align Left',
            justifyCenter: 'Align Center',
            justifyRight: 'Align Right',
            justifyFull: 'Align Justify',
            horizontalRule: 'Insert horizontal rule',
            removeformat: 'Remove format',
            fullscreen: 'Fullscreen',
            close: 'Close',
            submit: 'Confirm',
            reset: 'Cancel',
            required: 'Required',
            description: 'Description',
            title: 'Title',
            text: 'Text',
            target: 'Target'
        },
        ar : {
            _dir: "rtl",
            viewHTML: "إعرض-HTML",
            undo: "تراجع",
            redo: "إعادة",
            formatting: "تنسيق",
            p: "فقرة",
            blockquote: "اقتباس",
            code: "كود",
            header: "رأس",
            bold: "عريض",
            italic: "مائل",
            strikethrough: "مشطوب",
            underline: "خطّ سفلي",
            strong: "بارز",
            em: "تغميق",
            del: "حذف",
            superscript: "الأس",
            subscript: "أس سفلي",
            unorderedList: "قائمة غير مرتّبة",
            orderedList: "قائمة مرتّبة",
            insertImage: "إدراج صورة",
            insertVideo: "إدراج فيديو",
            link: "رابط",
            createLink: "انشاء رابط",
            unlink: "حذف رابط",
            justifyLeft: "تصحيح للشمال",
            justifyCenter: "توسيط",
            justifyRight: "تصحيح لليمين",
            justifyFull: "تصحيح لكلا الإتّجاهين",
            horizontalRule: "إدراج خطّ أفقي",
            fullscreen: "ملء الشاشة",
            close: "إغلاق",
            submit: "إرسال",
            reset: "إعادة تعيين",
            required: "إلزامي",
            description: "وصف",
            title: "عنوان",
            text: "نصّ",
            target: "الهدف"
        },
        bg : {
            viewHTML: "Прегледай HTML",
            formatting: "Форматиране",
            p: "Параграф",
            blockquote: "Цитат",
            code: "Код",
            header: "Заглавие",
            bold: "Удебелен",
            italic: "Наклонен",
            strikethrough: "Зачеркнат",
            underline: "Подчертан",
            strong: "Удебелен",
            em: "Наклонен",
            del: "Зачеркнат",
            unorderedList: "Обикновен списък",
            orderedList: "Номериран списък",
            insertImage: "Добави изображение",
            insertVideo: "Добави видео",
            link: "Връзка",
            createLink: "Създай връзка",
            unlink: "Премахни връзката",
            justifyLeft: "Подравни от ляво",
            justifyCenter: "Центрирай",
            justifyRight: "Подравни от дясно",
            justifyFull: "Подравни по ширина",
            horizontalRule: "Хоризонтална линия",
            fullscreen: "На цял екран",
            close: "Затвори",
            submit: "Впиши",
            reset: "Отмени",
            required: "Задължително",
            description: "Описание",
            title: "Заглавие",
            text: "Текст"
        },

        by:{
            viewHTML: "Паглядзець HTML",
            undo: "Скасаваць",
            redo: "Паўтарыць",
            formatting: "Фарматаванне",
            p: "Звычайны",
            blockquote: "Цытата",
            code: "Код",
            header: "Загаловак",
            bold: "Паўтлусты",
            italic: "Курсіў",
            strikethrough: "Закрэслены",
            underline: "Падкрэслены",
            strong: "Паўтлусты",
            em: "Курсіў",
            del: "Закрэслены",
            superscript: "Верхні індэкс",
            subscript: "Індэкс",
            unorderedList: "Звычайны спіс",
            orderedList: "Нумараваны спіс",
            insertImage: "Уставіць выяву",
            insertVideo: "Уставіць відэа",
            link: "Спасылка",
            createLink: "Уставіць спасылку",
            unlink: "Выдаліць спасылку",
            justifyLeft: "Па леваму боку",
            justifyCenter: "У цэнтры",
            justifyRight: "Па праваму боку",
            justifyFull: "Па шырыні",
            horizontalRule: "Гарызантальная лінія",
            removeformat: "Ачысціць фарматаванне",
            fullscreen: "На ўвесь экран",
            close: "Зачыніць",
            submit: "Уставіць",
            reset: "Скасаваць",
            required: "Абавязкова",
            description: "Апісанне",
            title: "Падказка",
            text: "Тэкст"
        },

        ca:{
            viewHTML: "Veure HTML",
            formatting: "Formatar",
            p: "Paragraf",
            blockquote: "Citació",
            code: "Codi",
            header: "Títol",
            bold: "Negreta",
            italic: "Itàlica",
            strikethrough: "Suprimir",
            underline: "Subratllat",
            strong: "Forta",
            em: "Èmfasi",
            del: "Apagar",
            unorderedList: "Lista desordenada",
            orderedList: "Lista ordenada",
            insertImage: "Inserir imatge",
            insertVideo: "Inserir vídeo",
            link: "Enllaç",
            createLink: "Crear un enllaç",
            unlink: "Eliminar enllaç",
            justifyLeft: "Alinear a esquerra",
            justifyCenter: "Centrar",
            justifyRight: "Alinear a dreta",
            justifyFull: "Justificar",
            horizontalRule: "Inserir separador horitzontal",
            fullscreen: "Pantalla completa",
            close: "Tancar",
            submit: "Enviar",
            reset: "Reiniciar",
            required: "Obligatori",
            description: "Descripció",
            title: "Títol",
            text: "Text"
        },

        cs:{
            viewHTML: "Zobrazit HTML",
            formatting: "Formátování",
            p: "Odstavec",
            blockquote: "Citace",
            code: "Kód",
            header: "Nadpis",
            bold: "Tučné",
            italic: "Kurzíva",
            strikethrough: "Přeškrtnuté",
            underline: "Podtržené",
            strong: "Tučné",
            em: "Zvýraznit",
            del: "Smazat",
            unorderedList: "Netříděný seznam",
            orderedList: "Tříděný seznam",
            insertImage: "Vložit obrázek",
            insertVideo: "Vložit video",
            link: "Odkaz",
            createLink: "Vložit odkaz",
            unlink: "Smazat odkaz",
            justifyLeft: "Zarovnat doleva",
            justifyCenter: "Zarovnat na střed",
            justifyRight: "Zarovnat doprava",
            justifyFull: "Zarovnat do bloku",
            horizontalRule: "Vložit vodorovnou čáru",
            fullscreen: "Režim celé obrazovky",
            close: "Zavřít",
            submit: "Potvrdit",
            reset: "Zrušit",
            required: "Povinné",
            description: "Popis",
            title: "Nadpis",
            text: "Text"
        },

        da:{
            viewHTML: "Vis HTML",
            formatting: "Formatter",
            p: "Afsnit",
            blockquote: "Citat",
            code: "Kode",
            header: "Overskrift",
            bold: "Fed",
            italic: "Kursiv",
            strikethrough: "Gennemstreg",
            underline: "Understreg",
            strong: "Vigtig",
            em: "Fremhæv",
            del: "Slettet",
            unorderedList: "Uordnet liste",
            orderedList: "Ordnet liste",
            insertImage: "Indsæt billede",
            insertVideo: "Indsæt video",
            link: "Link",
            createLink: "Indsæt link",
            unlink: "Fjern link",
            justifyLeft: "Venstrestil",
            justifyCenter: "Centrer",
            justifyRight: "Højrestil",
            justifyFull: "Lige margener",
            horizontalRule: "Horisontal linie",
            fullscreen: "Fuld skærm",
            close: "Luk",
            submit: "Bekræft",
            reset: "Annuller",
            required: "Påkrævet",
            description: "Beskrivelse",
            title: "Titel",
            text: "Tekst"
        },

        de:{
            viewHTML: "HTML anzeigen",
            formatting: "Formatieren",
            p: "Absatz",
            blockquote: "Zitat",
            code: "Code",
            header: "Überschrift",
            bold: "Fett",
            italic: "Kursiv",
            strikethrough: "Durchgestrichen",
            underline: "Unterstrichen",
            strong: "Wichtig",
            em: "Betont",
            del: "Gelöscht",
            unorderedList: "Ungeordnete Liste",
            orderedList: "Geordnete Liste",
            insertImage: "Bild einfügen",
            insertVideo: "Video einfügen",
            link: "Link",
            createLink: "Link einfügen",
            unlink: "Link entfernen",
            justifyLeft: "Links ausrichten",
            justifyCenter: "Zentrieren",
            justifyRight: "Rechts ausrichten",
            justifyFull: "Blocksatz",
            horizontalRule: "Horizontale Linie einfügen",
            fullscreen: "Vollbild",
            close: "Schliessen",
            submit: "Bestätigen",
            reset: "Rücksetzen",
            required: "Erforderlich",
            description: "Beschreibung",
            title: "Titel",
            text: "Text"
        },

        el:{
            viewHTML: "Προβολή κώδικα HTML",
            formatting: "Μορφοποίηση",
            p: "Παράγραφος",
            blockquote: "Παράθεση",
            code: "Κώδικας",
            header: "Επικεφαλίδα",
            bold: "Έντονα",
            italic: "Πλάγια",
            strikethrough: "Διαγραφή",
            underline: "Υπογράμμιση",
            strong: "Έντονα",
            em: "Πλάγια",
            del: "Διαγραφή",
            unorderedList: "Αταξινόμητη λίστα",
            orderedList: "Ταξινομημένη λίστα",
            insertImage: "Εισαγωγή εικόνας",
            insertVideo: "Εισαγωγή βίντεο",
            link: "Σύνδεσμος",
            createLink: "Δημιουργία συνδέσμου",
            unlink: "Διαγραφή συνδέσμου",
            justifyLeft: "Στοίχιση αριστερά",
            justifyCenter: "Στοίχιση στο κέντρο",
            justifyRight: "Στοίχιση δεξιά",
            justifyFull: "Πλήρης στοίχιση",
            horizontalRule: "Οριζόντια γραμμή",
            removeformat: "Καθαρισμός μορφοποίησης",
            fullscreen: "Πλήρης οθόνη",
            close: "Κλείσιμο",
            submit: "Υποβολή",
            reset: "Επαναφορά",
            required: "Απαραίτητο",
            description: "Περιγραφή",
            title: "Τίτλος",
            text: "Κείμενο"
        },

        es:{
            viewHTML: "Ver HTML",
            undo: "Deshacer",
            redo: "Rehacer",
            formatting: "Formato",
            p: "Párrafo",
            blockquote: "Cita",
            code: "Código",
            header: "Título",
            bold: "Negrita",
            italic: "Cursiva",
            strikethrough: "Tachado",
            underline: "Subrayado",
            strong: "Negrita",
            em: "Énfasis",
            del: "Borrar",
            superscript: "Sobrescrito",
            subscript: "Subíndice",
            unorderedList: "Lista Desordenada",
            orderedList: "Lista Ordenada",
            insertImage: "Insertar una imagen",
            insertVideo: "Insertar un vídeo",
            link: "Enlace",
            createLink: "Insertar un enlace",
            unlink: "Suprimir un enlace",
            justifyLeft: "Izquierda",
            justifyCenter: "Centrar",
            justifyRight: "Derecha",
            justifyFull: "Justificado",
            horizontalRule: "Insertar separador horizontal",
            removeformat: "Eliminar formato",
            fullscreen: "Pantalla completa",
            close: "Cerrar",
            submit: "Enviar",
            reset: "Cancelar",
            required: "Obligatorio",
            description: "Descripción",
            title: "Título",
            text: "Texto",
            target: "Target"
        },

        es_ar:{
            viewHTML: "Ver HTML",
            formatting: "Formato",
            p: "Párrafo",
            blockquote: "Cita",
            code: "Código",
            header: "Título",
            bold: "Negrita",
            italic: "Itálica",
            strikethrough: "Tachado",
            underline: "Subrayado",
            strong: "Fuere",
            em: "Énfasis",
            del: "Borrar",
            unorderedList: "Lista Desordenada",
            orderedList: "Lista Ordenada",
            insertImage: "Insertar una imagen",
            insertVideo: "Insertar un video",
            link: "Vínculo",
            createLink: "Insertar un vínculo",
            unlink: "Suprimir un vínculo",
            justifyLeft: "Alinear a la Izquierda",
            justifyCenter: "Centrar",
            justifyRight: "Alinear a la Derecha",
            justifyFull: "Justificado",
            horizontalRule: "Insertar separado Horizontal",
            fullscreen: "Pantalla Completa",
            close: "Cerrar",
            submit: "Enviar",
            reset: "Cancelar",
            required: "Obligatorio",
            description: "Descripción",
            title: "Título",
            text: "Texto"
        },

        fa:{
            viewHTML: "نمایش کد اچ تی ام ال",
            formatting: "قالب بندی",
            p: "پاراگراف",
            blockquote: "نقل قول",
            code: "کد",
            header: "سر تیتر",
            bold: "ضخیم",
            italic: "مورب",
            strikethrough: "میان خط دار",
            underline: "زیر خط دار",
            strong: "برجسته",
            em: "مورب",
            del: "حذف شده",
            unorderedList: "لیست نامرتب",
            orderedList: "لیست مرتب",
            insertImage: "درج تصویر",
            insertVideo: "درج ویدئو",
            link: "لینک",
            createLink: "درج لینک",
            unlink: "حذف لینک",
            justifyLeft: "تراز به چپ",
            justifyCenter: "تراز به وسط",
            justifyRight: "تراز به راست",
            justifyFull: "تراز به چپ و راست",
            horizontalRule: "درج خط افقی",
            fullscreen: "تمام صفحه",
            close: "بستن",
            submit: "تائید",
            reset: "انصراف",
            required: "اجباری",
            description: "توضیحات",
            title: "عنوان",
            text: "متن"
        },

        fi:{
            viewHTML: "Näytä HTML",
            undo: "Kumoa",
            redo: "Tee uudelleen",
            formatting: "Muotoilu",
            p: "Kappale",
            blockquote: "Lainaus",
            code: "Koodi",
            header: "Otsikko",
            bold: "Lihavointi",
            italic: "Kursivointi",
            strikethrough: "Yliviivaus",
            underline: "Allevivaus",
            strong: "Vahvennus",
            em: "Painotus",
            del: "Poistettu",
            unorderedList: "Luettelo",
            orderedList: "Numeroitu luettelo",
            insertImage: "Lisää kuva",
            insertVideo: "Lisää video",
            link: "Linkki",
            createLink: "Luo linkki",
            unlink: "Poista linkki",
            justifyLeft: "Tasaa vasemmalle",
            justifyCenter: "Keskitä",
            justifyRight: "Tasaa oikealle",
            justifyFull: "Tasaa",
            horizontalRule: "Vaakaviiva",
            fullscreen: "Kokoruutu",
            close: "Sulje",
            submit: "Lisää",
            reset: "Palauta",
            required: "Pakollinen",
            description: "Kuvaus",
            title: "Otsikko",
            text: "Teksti"
        },

        fr:{
            viewHTML: "Voir le HTML",
            undo: "Annuler",
            redo: "Refaire",
            formatting: "Format",
            p: "Paragraphe",
            blockquote: "Citation",
            code: "Code",
            header: "Titre",
            bold: "Gras",
            italic: "Italique",
            strikethrough: "Rayé",
            underline: "Souligné",
            strong: "Fort",
            em: "Emphase",
            del: "Supprimé",
            superscript: "Exposant",
            subscript: "Indice",
            unorderedList: "Liste à puces",
            orderedList: "Liste ordonnée",
            insertImage: "Insérer une image",
            insertVideo: "Insérer une video",
            link: "Lien",
            createLink: "Insérer un lien",
            unlink: "Supprimer le lien",
            justifyLeft: "Aligner à gauche",
            justifyCenter: "Centrer",
            justifyRight: "Aligner à droite",
            justifyFull: "Justifier",
            horizontalRule: "Insérer un séparateur horizontal",
            removeformat: "Supprimer formatage",
            fullscreen: "Plein écran",
            close: "Fermer",
            submit: "Valider",
            reset: "Annuler",
            required: "Obligatoire",
            description: "Description",
            title: "Titre",
            text: "Texte",
            target: "Cible"
        },

        he:{
            _dir: "rtl",
            viewHTML: "צפה ב-HTML",
            formatting: "פורמט",
            p: "פסקה",
            blockquote: "ציטוט",
            code: "קוד",
            header: "ראשית",
            bold: "מודגש",
            italic: "נטוי",
            strikethrough: "קו חוצה",
            underline: "קו תחתון",
            strong: "בולט",
            em: "הדגשה",
            del: "נמחק",
            unorderedList: "רשימה ללא סדר",
            orderedList: "רשימה מסודרת",
            insertImage: "הכנס תמונה",
            insertVideo: "הכנס סרטון",
            link: "קישור",
            createLink: "צור קישור",
            unlink: "הסר קישור",
            justifyLeft: "ישר לשמאל",
            justifyCenter: "מרכז",
            justifyRight: "ישר לימין",
            justifyFull: "ישר לשני הצדדים",
            horizontalRule: "הכנס קו אופקי",
            fullscreen: "מסך מלא",
            close: "סגור",
            submit: "שלח",
            reset: "אתחל מחדש",
            required: "נחוץ",
            description: "תיאור",
            title: "כותרת",
            text: "טקסט"
        },

        hr:{
            viewHTML: "Poglеdaj HTML kód",
            formatting: "Formatiranjе",
            p: "Odlomak",
            blockquote: "Citat",
            code: "Kód",
            header: "Zaglavlje",
            bold: "Podеbljano",
            italic: "Nakošeno",
            strikethrough: "Prеcrtano",
            underline: "Podvučеno",
            strong: "Podеbljano",
            em: "Istaknuto",
            del: "Obrisano",
            unorderedList: "Neuređen popis",
            orderedList: "Uređen popis",
            insertImage: "Dodaj sliku",
            insertVideo: "Dodaj vidеo",
            link: "Povezica",
            createLink: "Dodaj povezicu",
            unlink: "Ukloni povezicu",
            justifyLeft: "Lijеvo poravnanjе",
            justifyCenter: "Središnje poravnanjе",
            justifyRight: "Dеsno poravnanjе",
            justifyFull: "Obostrano poravnanjе",
            horizontalRule: "Horizontalna crta",
            fullscreen: "Puni zaslon",
            close: "Zatvori",
            submit: "Unеsi",
            reset: "Otkaži",
            required: "Obavеzno poljе",
            description: "Opis",
            title: "Naslov",
            text: "Tеkst"
        },

        hu:{
            viewHTML: "HTML nézet",
            formatting: "Stílusok",
            p: "Bekezdés",
            blockquote: "Idézet",
            code: "Kód",
            header: "Címsor",
            bold: "Félkövér",
            italic: "Dőlt",
            strikethrough: "Áthúzott",
            underline: "Aláhúzott",
            strong: "Vastag",
            em: "Kiemelt",
            del: "Törölt",
            unorderedList: "Felsorolás",
            orderedList: "Számozás",
            insertImage: "Kép beszúrása",
            insertVideo: "Video beszúrása",
            link: "Link",
            createLink: "Link létrehozása",
            unlink: "Link eltávolítása",
            justifyLeft: "Balra igazítás",
            justifyCenter: "Középre igazítás",
            justifyRight: "Jobbra igazítás",
            justifyFull: "Sorkizárt",
            horizontalRule: "Vízszintes vonal",
            fullscreen: "Teljes képernyő",
            close: "Bezár",
            submit: "Beküldés",
            reset: "Alaphelyzet",
            required: "Kötelező",
            description: "Leírás",
            title: "Cím",
            text: "Szöveg",
            removeformat: "Formázás eltávolítása"
        },

        id:{
            viewHTML: "Lihat HTML",
            formatting: "Penyusunan",
            p: "Paragraf",
            blockquote: "Kutipan",
            code: "Kode",
            header: "Kepala",
            bold: "Tebal",
            italic: "Miring",
            strikethrough: "Coret",
            underline: "Garis bawah",
            strong: "Tebal",
            em: "Miring",
            del: "Dicoret",
            unorderedList: "Daftar tak teratur",
            orderedList: "Daftar teratur",
            insertImage: "Sisipkan gambar",
            insertVideo: "Sisipkan video",
            link: "Tautan",
            createLink: "Sisipkan Tautan",
            unlink: "Singkirkan tautan",
            justifyLeft: "Rata kiri",
            justifyCenter: "Rata Tengah",
            justifyRight: "Rata kanan",
            justifyFull: "Rata kiri dan kanan",
            horizontalRule: "Sisipkan garis mendatar",
            fullscreen: "Layar penuh",
            close: "Tutup",
            submit: "Setuju",
            reset: "Batal",
            required: "Diperlukan",
            description: "Deskripsi",
            title: "Judul",
            text: "Teks"
        },

        it:{
            viewHTML: "Mostra HTML",
            formatting: "Formattazione",
            p: "Paragrafo",
            blockquote: "Citazione",
            code: "Codice",
            header: "Intestazione",
            bold: "Grassetto",
            italic: "Italico",
            strikethrough: "Barrato",
            underline: "Sottolineato",
            strong: "Rafforza",
            em: "Enfatizza",
            del: "Cancella",
            unorderedList: "Elenco puntato",
            orderedList: "Elenco numerato",
            insertImage: "Inserisci immagine",
            insertVideo: "Inserisci video",
            link: "Collegamento",
            createLink: "Crea un collegamento",
            unlink: "Elimina collegamento",
            justifyLeft: "Allinea a sinistra",
            justifyCenter: "Centra",
            justifyRight: "Allinea a destra",
            justifyFull: "Giustifica",
            horizontalRule: "Inserisci un separatore orizzontale",
            fullscreen: "Schermo intero",
            close: "Chiudi",
            submit: "Invia",
            reset: "Annulla",
            required: "Obbligatorio",
            description: "Descrizione",
            title: "Titolo",
            text: "Testo",
            removeformat: "Rimuovi Formattazione",
            superscript: "Apice",
            subscript: "Pedice"
        },

        ja:{
            viewHTML: "HTML表示",
            undo: "元に戻す",
            redo: "やり直す",
            formatting: "フォーマット",
            p: "段落",
            blockquote: "引用",
            code: "コード",
            header: "見出し",
            bold: "太字",
            italic: "斜体",
            strikethrough: "取り消し線",
            underline: "下線",
            strong: "太字",
            em: "斜体",
            del: "取り消し線",
            superscript: "上付き文字",
            subscript: "下付き文字",
            unorderedList: "順序なしリスト",
            orderedList: "順序ありリスト",
            insertImage: "画像の挿入",
            link: "リンク",
            createLink: "リンクの作成",
            unlink: "リンクの削除",
            justifyLeft: "左揃え",
            justifyCenter: "中央揃え",
            justifyRight: "右揃え",
            justifyFull: "両端揃え",
            horizontalRule: "横罫線",
            removeformat: "フォーマットの削除",
            fullscreen: "全画面表示",
            close: "閉じる",
            submit: "送信",
            reset: "キャンセル",
            required: "必須",
            description: "説明",
            title: "タイトル",
            text: "テキスト",
            target: "ターゲット"
        },

        ko:{
            viewHTML: "HTML로 보기",
            formatting: "양식",
            p: "문단",
            blockquote: "인용부호",
            code: "코드",
            header: "머릿말",
            bold: "진하게",
            italic: "기울임",
            strikethrough: "취소선",
            underline: "밑줄",
            strong: "굵게",
            em: "강조",
            del: "취소",
            unorderedList: "순차 목록",
            orderedList: "비순차 목록",
            insertImage: "이미지 넣기",
            insertVideo: "비디오 넣기",
            link: "링크",
            createLink: "링크 넣기",
            unlink: "링크 없애기",
            justifyLeft: "왼쪽 정렬",
            justifyCenter: "가운데 정렬",
            justifyRight: "오른쪽 정렬",
            justifyFull: "혼합 정렬",
            horizontalRule: "가로줄 넣기",
            fullscreen: "전체 화면",
            close: "닫기",
            submit: "전송",
            reset: "초기화",
            required: "꼭 입력해야 합니다.",
            description: "설명",
            title: "제목",
            text: "본문 내용"
        },

        mn:{
            viewHTML: "HTML харах",
            undo: "Буцаах",
            redo: "Дахих",
            formatting: "Формат",
            p: "Догол мөр",
            blockquote: "Ишлэл",
            code: "Код",
            header: "Гарчиг",
            bold: "Тод",
            italic: "Налуу",
            strikethrough: "Дундуур зураас",
            underline: "Доогуур зураас",
            strong: "Тод",
            em: "Налуу",
            del: "Дундуур зураас",
            superscript: "Дээд индекс",
            subscript: "Доод индекс",
            unorderedList: "Дугаарлаагүй жагсаалт",
            orderedList: "Дугаарласан жагсаалт",
            insertImage: "Зураг оруулах",
            insertVideo: "Видео оруулах",
            link: "Холбоос",
            createLink: "Холбоос үүсгэх",
            unlink: "Холбоос цуцлах",
            justifyLeft: "Зүүн тийш шахах",
            justifyCenter: "Голлуулах",
            justifyRight: "Баруун Баруун тийш шахах",
            justifyFull: "Тэгшитгэх",
            horizontalRule: "Хөндлөн шугам",
            removeformat: "Формат арилгах",
            fullscreen: "Дэлгэц дүүргэх",
            close: "Хаах",
            submit: "Оруулах",
            reset: "Цуцлах",
            required: "Шаардлагатай",
            description: "Тайлбар",
            title: "Гарчиг",
            text: "Текст",
            target: "Бай"
        },

        my:{
            viewHTML: "Lihat HTML",
            formatting: "Pemformatan",
            p: "Perenggan",
            blockquote: "Blockquote",
            code: "Kod",
            header: "Pengepala",
            bold: "Tebal",
            italic: "Condong",
            strikethrough: "Garis batal",
            underline: "Garis bawah",
            strong: "Kuat",
            em: "Condong",
            del: "Hapus",
            unorderedList: "Senarai tidak tertib",
            orderedList: "Senarai tertib",
            insertImage: "Masukkan imej",
            insertVideo: "Masukkan video",
            link: "Pautan",
            createLink: "Cipta pautan",
            unlink: "Hapus pautan",
            justifyLeft: "Mengimbangkan ke kiri",
            justifyCenter: "Mengimbangkan ke tengah",
            justifyRight: "Mengimbangkan ke kanan",
            justifyFull: "Mengimbangkan ke kiri dan kanan",
            horizontalRule: "Masukkan garis mendatar",
            fullscreen: "Skrin penuh",
            close: "Tutup",
            submit: "Hantar",
            reset: "Batal",
            required: "Diperlukan",
            description: "Perihal",
            title: "Tajuk",
            text: "Teks"
        },

        nl:{
            viewHTML: "HTML bekijken",
            formatting: "Opmaak",
            p: "Paragraaf",
            blockquote: "Citaat",
            code: "Code",
            header: "Kop",
            bold: "Vet",
            italic: "Cursief",
            strikethrough: "Doorhalen",
            underline: "Onderlijnen",
            strong: "Sterk",
            em: "Nadruk",
            del: "Verwijderd",
            unorderedList: "Ongenummerde lijst",
            orderedList: "Genummerde lijst",
            insertImage: "Afbeelding invoegen",
            insertVideo: "Video invoegen",
            link: "Link",
            createLink: "Link maken",
            unlink: "Link verwijderen",
            justifyLeft: "Links uitlijnen",
            justifyCenter: "Centreren",
            justifyRight: "Rechts uitlijnen",
            justifyFull: "Uitvullen",
            horizontalRule: "Horizontale lijn",
            removeFormat: "Opmaak verwijderen",
            fullscreen: "Volledig scherm",
            close: "Sluiten",
            submit: "Verzenden",
            reset: "Herstellen",
            required: "Verplicht",
            description: "Omschrijving",
            title: "Titel",
            text: "Tekst"
        },

        no_nb:{
            viewHTML: "Vis HTML",
            formatting: "Formater",
            p: "Avsnitt",
            blockquote: "Sitat",
            code: "Kode",
            header: "Overskrift",
            bold: "Fet",
            italic: "Kursiv",
            strikethrough: "Gjennomstreking",
            underline: "Understreking",
            strong: "Viktig",
            em: "Fremhevet",
            del: "Slettet",
            unorderedList: "Uordnet liste",
            orderedList: "Ordnet liste",
            insertImage: "Sett inn bilde",
            insertVideo: "Sett inn video",
            link: "Lenke",
            createLink: "Sett inn lenke",
            unlink: "Fjern lenke",
            justifyLeft: "Venstrejuster",
            justifyCenter: "Midtstill",
            justifyRight: "Høyrejuster",
            justifyFull: "Blokkjuster",
            horizontalRule: "Horisontal linje",
            fullscreen: "Full skjerm",
            close: "Lukk",
            submit: "Bekreft",
            reset: "Avbryt",
            required: "Påkrevd",
            description: "Beskrivelse",
            title: "Tittel",
            text: "Tekst"
        },

        ph:{
            viewHTML: "Tumingin sa HTML",
            formatting: "Formatting",
            p: "Talata",
            blockquote: "Blockquote",
            code: "Kowd",
            header: "Header",
            bold: "Makapal",
            italic: "Hilig",
            strikethrough: "Strikethrough",
            underline: "Salungguhit",
            strong: "Malakas",
            em: "Hilig",
            del: "Tinanggal",
            unorderedList: "Hindi nakahanay na listahan",
            orderedList: "Nakahanay na listahan",
            insertImage: "Ilagay ang larawan",
            insertVideo: "Ilagay ang video",
            link: "Koneksyon",
            createLink: "Iugnay",
            unlink: "Tanggalin ang koneksyon",
            justifyLeft: "Ihanay sa kaliwa",
            justifyCenter: "Ihanay sa gitna",
            justifyRight: "Ihanay sa kanan",
            justifyFull: "Ihanay sa kaliwa at kanan",
            horizontalRule: "Pahalang na linya",
            fullscreen: "Fullscreen",
            close: "Isara",
            submit: "Ipasa",
            reset: "I-reset",
            required: "Kailangan",
            description: "Paglalarawan",
            title: "Pamagat",
            text: "Teksto"
        },

        pl:{
            viewHTML: "Pokaż HTML",
            formatting: "Format",
            p: "Akapit",
            blockquote: "Cytat",
            code: "Kod",
            header: "Nagłówek",
            bold: "Pogrubienie",
            italic: "Pochylenie",
            strikethrough: "Przekreślenie",
            underline: "Podkreślenie",
            strong: "Wytłuszczenie",
            em: "Uwydatnienie",
            del: "Usunięte",
            unorderedList: "Lista nieuporządkowana",
            orderedList: "Lista uporządkowana",
            insertImage: "Wstaw obraz",
            insertVideo: "Wstaw film",
            link: "Link",
            createLink: "Wstaw link",
            unlink: "Usuń link",
            justifyLeft: "Wyrównaj do lewej",
            justifyCenter: "Wyśrodkuj",
            justifyRight: "Wyrównaj do prawej",
            justifyFull: "Wyjustuj",
            horizontalRule: "Odkreśl linią",
            fullscreen: "Pełny ekran",
            close: "Zamknij",
            submit: "Zastosuj",
            reset: "Przywróć",
            required: "Wymagane",
            description: "Opis",
            title: "Tytuł",
            text: "Tekst"
        },

        pt:{
            viewHTML: "Ver HTML",
            undo: "Desfazer",
            redo: "Refazer",
            formatting: "Formatar",
            p: "Paragráfo",
            blockquote: "Citação",
            code: "Código",
            header: "Título",
            bold: "Negrito",
            italic: "Itálico",
            strikethrough: "Suprimir",
            underline: "Sublinhado",
            strong: "Negrito",
            em: "Ênfase",
            del: "Apagar",
            superscript: "Sobrescrito",
            subscript: "Subscrito",
            unorderedList: "Lista não ordenada",
            orderedList: "Lista ordenada",
            insertImage: "Inserir imagem",
            insertVideo: "Inserir vídeo",
            link: "Link",
            createLink: "Criar um link",
            unlink: "Remover link",
            justifyLeft: "Alinhar a esquerda",
            justifyCenter: "Centralizar",
            justifyRight: "Alinhar a direita",
            justifyFull: "Justificar",
            horizontalRule: "Inserir separador horizontal",
            removeformat: "Remover formatação",
            fullscreen: "Tela cheia",
            close: "Fechar",
            submit: "Enviar",
            reset: "Limpar",
            required: "Obrigatório",
            description: "Descrição",
            title: "Título",
            text: "Texto",
            target: "Target"
        },

        pt_br:{
            viewHTML: "Ver HTML",
            undo: "Desfazer",
            redo: "Refazer",
            formatting: "Formatar",
            p: "Parágrafo",
            blockquote: "Citação",
            code: "Código",
            header: "Título",
            bold: "Negrito",
            italic: "Itálico",
            strikethrough: "Tachado",
            underline: "Sublinhado",
            strong: "Negrito",
            em: "Ênfase",
            del: "Apagar",
            superscript: "Sobrescrito",
            subscript: "Subscrito",
            unorderedList: "Lista não ordenada",
            orderedList: "Lista ordenada",
            insertImage: "Inserir imagem",
            insertVideo: "Inserir vídeo",
            link: "Link",
            createLink: "Criar um link",
            unlink: "Remover link",
            justifyLeft: "Alinhar a esquerda",
            justifyCenter: "Centralizar",
            justifyRight: "Alinhar a direita",
            justifyFull: "Justificar",
            horizontalRule: "Inserir separador horizontal",
            removeformat: "Remover formatação",
            fullscreen: "Tela cheia",
            close: "Fechar",
            submit: "Enviar",
            reset: "Limpar",
            required: "Obrigatório",
            description: "Descrição",
            title: "Título",
            text: "Texto",
            target: "Target"
        },

        ro:{
            viewHTML: "Vizualizare HTML",
            formatting: "Format",
            p: "Paragraf",
            blockquote: "Citație",
            code: "Cod",
            header: "Titlu",
            bold: "Bold",
            italic: "Italic",
            strikethrough: "Tăiat",
            underline: "Subliniat",
            strong: "Puternic",
            em: "Accentuat",
            del: "Sterge",
            unorderedList: "Lista dezordonată",
            orderedList: "Liste ordonată",
            insertImage: "Adăugare Imagine",
            insertVideo: "Adăugare Video",
            link: "Link",
            createLink: "Crează link",
            unlink: "Remover link",
            justifyLeft: "Aliniază stânga",
            justifyCenter: "Aliniază centru",
            justifyRight: "Aliniază dreapta",
            justifyFull: "Justificare",
            horizontalRule: "Linie orizontală",
            fullscreen: "Tot ecranul",
            close: "Închide",
            submit: "Procesează",
            reset: "Resetează",
            required: "Obligatoriu",
            description: "Descriere",
            title: "Titlu",
            text: "Text"
        },

        rs:{
            viewHTML: "Погледај HTML кóд",
            formatting: "Форматирање",
            p: "Параграф",
            blockquote: "Цитат",
            code: "Кóд",
            header: "Наслов",
            bold: "Подебљано",
            italic: "Курзив",
            strikethrough: "Прецртано",
            underline: "Подвучено",
            strong: "Подебљано",
            em: "Истакнуто",
            del: "Обрисано",
            unorderedList: "Ненабројива листа",
            orderedList: "Набројива листа",
            insertImage: "Унеси слику",
            insertVideo: "Унеси видео",
            link: "Линк",
            createLink: "Унеси линк",
            unlink: "Уклони линк",
            justifyLeft: "Лево равнање",
            justifyCenter: "Централно равнање",
            justifyRight: "Десно равнање",
            justifyFull: "Обострано равнање",
            horizontalRule: "Хоризонтална линија",
            fullscreen: "Режим читавог екрана",
            close: "Затвори",
            submit: "Унеси",
            reset: "Откажи",
            required: "Обавезно поље",
            description: "Опис",
            title: "Наслов",
            text: "Текст"
        },

        rs_latin:{
            viewHTML: "Poglеdaj HTML kód",
            formatting: "Formatiranjе",
            p: "Paragraf",
            blockquote: "Citat",
            code: "Kód",
            header: "Naslov",
            bold: "Podеbljano",
            italic: "Kurziv",
            strikethrough: "Prеcrtano",
            underline: "Podvučеno",
            strong: "Podеbljano",
            em: "Istaknuto",
            del: "Obrisano",
            unorderedList: "Nеnabrojiva lista",
            orderedList: "Nabrojiva lista",
            insertImage: "Unеsi sliku",
            insertVideo: "Unеsi vidеo",
            link: "Link",
            createLink: "Unеsi link",
            unlink: "Ukloni link",
            justifyLeft: "Lеvo ravnanjе",
            justifyCenter: "Cеntralno ravnanjе",
            justifyRight: "Dеsno ravnanjе",
            justifyFull: "Obostrano ravnanjе",
            horizontalRule: "Horizontalna linija",
            fullscreen: "Rеžim čitavog еkrana",
            close: "Zatvori",
            submit: "Unеsi",
            reset: "Otkaži",
            required: "Obavеzno poljе",
            description: "Opis",
            title: "Naslov",
            text: "Tеkst"
        },

        ru:{
            viewHTML: "Посмотреть HTML",
            undo: "Отменить",
            redo: "Повторить",
            formatting: "Форматирование",
            p: "Обычный",
            blockquote: "Цитата",
            code: "Код",
            header: "Заголовок",
            bold: "Полужирный",
            italic: "Курсив",
            strikethrough: "Зачеркнутый",
            underline: "Подчеркнутый",
            strong: "Полужирный",
            em: "Курсив",
            del: "Зачеркнутый",
            superscript: "Надстрочный",
            subscript: "Подстрочный",
            unorderedList: "Обычный список",
            orderedList: "Нумерованный список",
            insertImage: "Вставить изображение",
            insertVideo: "Вставить видео",
            link: "Ссылка",
            createLink: "Вставить ссылку",
            unlink: "Удалить ссылку",
            justifyLeft: "По левому краю",
            justifyCenter: "По центру",
            justifyRight: "По правому краю",
            justifyFull: "По ширине",
            horizontalRule: "Горизонтальная линия",
            removeformat: "Очистить форматирование",
            fullscreen: "Во весь экран",
            close: "Закрыть",
            submit: "Вставить",
            reset: "Отменить",
            required: "Обязательное",
            description: "Описание",
            title: "Подсказка",
            text: "Текст"
        },

        sk:{
            viewHTML: "Zobraziť HTML",
            formatting: "Formátovanie",
            p: "Paragraf",
            blockquote: "Citácia",
            code: "Kód",
            header: "Nadpis",
            bold: "Tučné",
            italic: "Kurzíva",
            strikethrough: "Preškrtnuté",
            underline: "Podčiarknuté",
            strong: "Tučné",
            em: "Zvýrazniť",
            del: "Zmazať",
            unorderedList: "Netriedený zoznam",
            orderedList: "Triedený zoznam",
            insertImage: "Vložiť obrázok",
            insertVideo: "Vložiť video",
            link: "Odkaz",
            createLink: "Vložiť odkaz",
            unlink: "Zmazať odkaz",
            justifyLeft: "Zarovnať doľava",
            justifyCenter: "Zarovnať na stred",
            justifyRight: "Zarovnať doprava",
            justifyFull: "Zarovnať do bloku",
            horizontalRule: "Vložit vodorovnú čiaru",
            fullscreen: "Režim celej obrazovky",
            close: "Zavrieť",
            submit: "Potvrdiť",
            reset: "Zrušiť",
            required: "Povinné",
            description: "Popis",
            title: "Nadpis",
            text: "Text"
        },

        sv:{
            viewHTML: "Visa HTML",
            formatting: "Formatering",
            p: "Paragraf",
            blockquote: "Citat",
            code: "Kod",
            header: "Rubrik",
            bold: "Fet",
            italic: "Kursiv",
            strikethrough: "Genomstruken",
            underline: "Understruken",
            strong: "Fet",
            em: "Kursiv",
            del: "Rensa formatering",
            unorderedList: "Punktlista",
            orderedList: "Numrerad lista",
            insertImage: "Infoga bild",
            insertVideo: "Infoga video",
            link: "Länk",
            createLink: "Infoga länk",
            unlink: "Ta bort länk",
            justifyLeft: "Vänsterjustera",
            justifyCenter: "Centrera",
            justifyRight: "Högerjustera",
            justifyFull: "Marginaljustera",
            horizontalRule: "Horisontell linje",
            fullscreen: "Fullskärm",
            close: "Stäng",
            submit: "Bekräfta",
            reset: "Återställ",
            required: "Obligatorisk",
            description: "Beskrivning",
            title: "Titel",
            text: "Text"
        },

        tr:{
            viewHTML: "HTML Kodu",
            formatting: "Biçimlendirme",
            p: "Paragraf",
            blockquote: "Alıntı",
            code: "Kod",
            header: "Başlık",
            bold: "Kalın",
            italic: "İtalik",
            strikethrough: "Üzeri çizgili",
            underline: "Altı çizgili",
            strong: "Koyu",
            em: "Vurgulu",
            del: "Üzeri çizgili",
            unorderedList: "Simgeli liste",
            orderedList: "Numaralı liste",
            insertImage: "Resim yerleştir",
            insertVideo: "Video yerleştir",
            link: "Link",
            createLink: "Link yerleştir",
            unlink: "Linki sil",
            justifyLeft: "Sola hizala",
            justifyCenter: "Ortaya hizala",
            justifyRight: "Sağa hizala",
            justifyFull: "Yasla",
            horizontalRule: "Yatay satır ekle",
            fullscreen: "Tam ekran",
            close: "Kapat",
            submit: "Onayla",
            reset: "Sıfırla",
            required: "Gerekli",
            description: "Açıklama",
            title: "Başlık",
            text: "Metin"
        },

        ua:{
            viewHTML: "Подивитись HTML",
            formatting: "Форматування",
            p: "Звичайний",
            blockquote: "Витяг",
            code: "Код",
            header: "Заголовок",
            bold: "Напівжирний",
            italic: "Курсив",
            strikethrough: "Закреслений",
            underline: "Підкреслений",
            strong: "Напівжирний",
            em: "Курсив",
            del: "Закреслений",
            unorderedList: "Звичайний список",
            orderedList: "Нумерований список",
            insertImage: "Вставити зображення",
            insertVideo: "Вставити відео",
            link: "Посилання",
            createLink: "Вставити посилання",
            unlink: "Видалити посилання",
            justifyLeft: "По лівому краю",
            justifyCenter: "В центрі",
            justifyRight: "По правому краю",
            justifyFull: "По ширині",
            horizontalRule: "Горизонтальна лінія",
            fullscreen: "На весь екран",
            close: "Закрити",
            submit: "Вставити",
            reset: "Скасувати",
            required: "Обов'язкове",
            description: "Опис",
            title: "Підказка",
            text: "Текст"
        },

        vi:{
            viewHTML: "Hiển thị HTML",
            formatting: "Định dạng",
            p: "Đoạn",
            blockquote: "Trích dẫn",
            code: "Code",
            header: "Đầu trang",
            bold: "In đậm",
            italic: "In nghiêng",
            strikethrough: "Gạch ngang",
            underline: "Gạch chân",
            strong: "In đậm",
            em: "In nghiêng",
            del: "Gạch ngang",
            unorderedList: "Danh sách không thứ tự",
            orderedList: "Danh sách có thứ tự",
            insertImage: "Chèn hình ảnh",
            insertVideo: "Chèn video",
            link: "Đường dẫn",
            createLink: "Tạo đường dẫn",
            unlink: "Hủy đường dẫn",
            justifyLeft: "Canh lề trái",
            justifyCenter: "Canh giữa",
            justifyRight: "Canh lề phải",
            justifyFull: "Canh đều",
            horizontalRule: "Thêm đường kẻ ngang",
            fullscreen: "Toàn màn hình",
            close: "Đóng",
            submit: "Đồng ý",
            reset: "Hủy bỏ",
            required: "Bắt buộc",
            description: "Mô tả",
            title: "Tiêu đề",
            text: "Nội dung"
        },

        zh_cn:{
            viewHTML: "源代码",
            formatting: "格式",
            p: "段落",
            blockquote: "引用",
            code: "代码",
            header: "标题",
            bold: "加粗",
            italic: "斜体",
            strikethrough: "删除线",
            underline: "下划线",
            strong: "加粗",
            em: "斜体",
            del: "删除线",
            unorderedList: "无序列表",
            orderedList: "有序列表",
            insertImage: "插入图片",
            insertVideo: "插入视频",
            link: "超链接",
            createLink: "插入链接",
            unlink: "取消链接",
            justifyLeft: "居左对齐",
            justifyCenter: "居中对齐",
            justifyRight: "居右对齐",
            justifyFull: "两端对齐",
            horizontalRule: "插入分隔线",
            fullscreen: "全屏",
            close: "关闭",
            submit: "确定",
            reset: "取消",
            required: "必需的",
            description: "描述",
            title: "标题",
            text: "文字"
        },

        zh_tw:{
            viewHTML: "原始碼",
            formatting: "格式",
            p: "段落",
            blockquote: "引用",
            code: "代碼",
            header: "標題",
            bold: "加粗",
            italic: "斜體",
            strikethrough: "刪除線",
            underline: "底線",
            strong: "加粗",
            em: "斜體",
            del: "刪除線",
            unorderedList: "無序列表",
            orderedList: "有序列表",
            insertImage: "插入圖片",
            insertVideo: "插入影片",
            link: "超連結",
            createLink: "插入連結",
            unlink: "取消連結",
            justifyLeft: "靠左對齊",
            justifyCenter: "置中對齊",
            justifyRight: "靠右對齊",
            justifyFull: "左右對齊",
            horizontalRule: "插入分隔線",
            fullscreen: "全螢幕",
            close: "關閉",
            submit: "確定",
            reset: "取消",
            required: "必需的",
            description: "描述",
            title: "標題",
            text: "文字"
        }
    },

    // Plugins
    plugins: {},

    // SVG Path globally
    svgPath: null,

    hideButtonTexts: null
};


(function (navigator, window, document, $) {
    'use strict';

    $.fn.trumbowyg = function (options, params) {
        var trumbowygDataName = 'trumbowyg';
        if (options === Object(options) || !options) {
            return this.each(function () {
                if (!$(this).data(trumbowygDataName)) {
                    $(this).data(trumbowygDataName, new Trumbowyg(this, options));
                }
            });
        }
        if (this.length === 1) {
            try {
                var t = $(this).data(trumbowygDataName);
                switch (options) {
                    // Exec command
                    case 'execCmd':
                        return t.execCmd(params.cmd, params.param, params.forceCss);

                    // Modal box
                    case 'openModal':
                        return t.openModal(params.title, params.content);
                    case 'closeModal':
                        return t.closeModal();
                    case 'openModalInsert':
                        return t.openModalInsert(params.title, params.fields, params.callback);

                    // Range
                    case 'saveRange':
                        return t.saveRange();
                    case 'getRange':
                        return t.range;
                    case 'getRangeText':
                        return t.getRangeText();
                    case 'restoreRange':
                        return t.restoreRange();

                    // Enable/disable
                    case 'enable':
                        return t.toggleDisable(false);
                    case 'disable':
                        return t.toggleDisable(true);

                    // Destroy
                    case 'destroy':
                        return t.destroy();

                    // Empty
                    case 'empty':
                        return t.empty();

                    // HTML
                    case 'html':
                        return t.html(params);
                }
            } catch (c) {
            }
        }

        return false;
    };

    // @param: editorElem is the DOM element
    var Trumbowyg = function (editorElem, options) {
        var t = this,
            trumbowygIconsId = 'trumbowyg-icons';

        // Get the document of the element. It use to makes the plugin
        // compatible on iframes.
        t.doc = editorElem.ownerDocument || document;

        // jQuery object of the editor
        t.$ta = $(editorElem); // $ta : Textarea
        t.$c = $(editorElem); // $c : creator

        options = options || {};

        // Localization management
        if (options.lang != null || $.trumbowyg.langs[options.lang] != null) {
            t.lang = $.extend(true, {}, $.trumbowyg.langs.en, $.trumbowyg.langs[options.lang]);
        } else {
            t.lang = $.trumbowyg.langs.en;
        }

        t.hideButtonTexts = $.trumbowyg.hideButtonTexts != null ? $.trumbowyg.hideButtonTexts : options.hideButtonTexts;

        // SVG path
        var svgPathOption = $.trumbowyg.svgPath != null ? $.trumbowyg.svgPath : options.svgPath;
        t.hasSvg = svgPathOption !== false;
        t.svgPath = !!t.doc.querySelector('base') ? window.location.href.split('#')[0] : '';
        if ($('#' + trumbowygIconsId, t.doc).length === 0 && svgPathOption !== false) {
            if (svgPathOption == null) {
                try {
                    throw new Error();
                } catch (e) {
                    var stackLines = e.stack.split('\n');

                    for (var i in stackLines) {
                        if (!stackLines[i].match(/http[s]?:\/\//)) {
                            continue;
                        }
                        svgPathOption = stackLines[Number(i)].match(/((http[s]?:\/\/.+\/)([^\/]+\.js))(\?.*)?:/)[1].split('/');
                        svgPathOption.pop();
                        svgPathOption = svgPathOption.join('/') + '/ui/icons.svg';
                        break;
                    }
                }
            }

            var div = t.doc.createElement('div');
            div.id = trumbowygIconsId;
            t.doc.body.insertBefore(div, t.doc.body.childNodes[0]);
            $.ajax({
                async: true,
                type: 'GET',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                dataType: 'xml',
                crossDomain: true,
                url: svgPathOption,
                data: null,
                beforeSend: null,
                complete: null,
                success: function (data) {
                    div.innerHTML = new XMLSerializer().serializeToString(data.documentElement);
                }
            });
        }


        /**
         * When the button is associated to a empty object
         * fn and title attributs are defined from the button key value
         *
         * For example
         *      foo: {}
         * is equivalent to :
         *      foo: {
             *          fn: 'foo',
             *          title: this.lang.foo
             *      }
         */
        var h = t.lang.header, // Header translation
            isBlinkFunction = function () {
                return (window.chrome || (window.Intl && Intl.v8BreakIterator)) && 'CSS' in window;
            };
        t.btnsDef = {
            viewHTML: {
                fn: 'toggle'
            },

            undo: {
                isSupported: isBlinkFunction,
                key: 'Z'
            },
            redo: {
                isSupported: isBlinkFunction,
                key: 'Y'
            },

            p: {
                fn: 'formatBlock'
            },
            blockquote: {
                fn: 'formatBlock'
            },
            h1: {
                fn: 'formatBlock',
                title: h + ' 1'
            },
            h2: {
                fn: 'formatBlock',
                title: h + ' 2'
            },
            h3: {
                fn: 'formatBlock',
                title: h + ' 3'
            },
            h4: {
                fn: 'formatBlock',
                title: h + ' 4'
            },
            subscript: {
                tag: 'sub'
            },
            superscript: {
                tag: 'sup'
            },

            bold: {
                key: 'B',
                tag: 'b'
            },
            italic: {
                key: 'I',
                tag: 'i'
            },
            underline: {
                tag: 'u'
            },
            strikethrough: {
                tag: 'strike'
            },

            strong: {
                fn: 'bold',
                key: 'B'
            },
            em: {
                fn: 'italic',
                key: 'I'
            },
            del: {
                fn: 'strikethrough'
            },

            createLink: {
                key: 'K',
                tag: 'a'
            },
            unlink: {},

            insertImage: {},

            justifyLeft: {
                tag: 'left',
                forceCss: true
            },
            justifyCenter: {
                tag: 'center',
                forceCss: true
            },
            justifyRight: {
                tag: 'right',
                forceCss: true
            },
            justifyFull: {
                tag: 'justify',
                forceCss: true
            },

            unorderedList: {
                fn: 'insertUnorderedList',
                tag: 'ul'
            },
            orderedList: {
                fn: 'insertOrderedList',
                tag: 'ol'
            },

            horizontalRule: {
                fn: 'insertHorizontalRule'
            },

            removeformat: {},

            fullscreen: {
                class: 'trumbowyg-not-disable'
            },
            close: {
                fn: 'destroy',
                class: 'trumbowyg-not-disable'
            },

            // Dropdowns
            formatting: {
                dropdown: ['p', 'blockquote', 'h1', 'h2', 'h3', 'h4'],
                ico: 'p'
            },
            link: {
                dropdown: ['createLink', 'unlink']
            }
        };

        // Defaults Options
        t.o = $.extend(true, {}, {
            lang: 'en',

            fixedBtnPane: false,
            fixedFullWidth: false,
            autogrow: false,

            prefix: 'trumbowyg-',

            semantic: true,
            resetCss: false,
            removeformatPasted: false,
            tagsToRemove: [],

            //btnsGrps: {
            //    design: ['bold', 'italic', 'underline', 'strikethrough'],
            //    semantic: ['strong', 'em', 'del'],
            //    justify: ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            //    lists: ['unorderedList', 'orderedList']
            //},
            //btns: [
            //    ['viewHTML'],
            //    ['undo', 'redo'],
            //    ['formatting'],
            //    'btnGrp-semantic',
            //    ['superscript', 'subscript'],
            //    ['foreColor', 'backColor'],
            //    ['link'],
            //    ['insertImage'],
            //    'btnGrp-justify',
            //    'btnGrp-lists',
            //    ['horizontalRule'],
            //    ['removeformat'],
            //    ['fullscreen']
            //],

            // For custom button definitions
            btnsDef: {},

            inlineElementsSelector: 'a,abbr,acronym,b,caption,cite,code,col,dfn,dir,dt,dd,em,font,hr,i,kbd,li,q,span,strikeout,strong,sub,sup,u',

            pasteHandlers: [],

            imgDblClickHandler: function () {
                var $img = $(this),
                    src = $img.attr('src'),
                    base64 = '(Base64)';

                if (src.indexOf('data:image') === 0) {
                    src = base64;
                }

                t.openModalInsert(t.lang.insertImage, {
                    url: {
                        label: 'URL',
                        value: src,
                        required: true
                    },
                    alt: {
                        label: t.lang.description,
                        value: $img.attr('alt')
                    }
                }, function (v) {
                    if (v.src !== base64) {
                        $img.attr({
                            src: v.src
                        });
                    }
                    $img.attr({
                        alt: v.alt
                    });
                    return true;
                });
                return false;
            },

            plugins: {}
        }, options);

        t.disabled = t.o.disabled || (editorElem.nodeName === 'TEXTAREA' && editorElem.disabled);

        if (options.btns) {
            t.o.btns = options.btns;
        } else if (!t.o.semantic) {
            t.o.btns[4] = 'btnGrp-design';
        }

        $.each(t.o.btnsDef, function (btnName, btnDef) {
            t.addBtnDef(btnName, btnDef);
        });

        // put this here in the event it would be merged in with options
        t.eventNamespace = 'trumbowyg-event';

        // Keyboard shortcuts are load in this array
        t.keys = [];

        // Tag to button dynamically hydrated
        t.tagToButton = {};
        t.tagHandlers = [];

        // Admit multiple paste handlers
        t.pasteHandlers = [].concat(t.o.pasteHandlers);

        // Check if browser is IE
        t.isIE = (navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') !== -1);

        t.init();
    };

    Trumbowyg.prototype = {
        init: function () {
            var t = this;
            t.height = t.$ta.height();

            t.initPlugins();

            try {
                // Disable image resize, try-catch for old IE
                t.doc.execCommand('enableObjectResizing', false, false);
                t.doc.execCommand('defaultParagraphSeparator', false, 'p');
            } catch (e) {
            }

            t.buildEditor();
            t.buildBtnPane();

            t.fixedBtnPaneEvents();

            t.buildOverlay();

            setTimeout(function () {
                if (t.disabled) {
                    t.toggleDisable(true);
                }
                t.$c.trigger('tbwinit');
            });
        },

        addBtnDef: function (btnName, btnDef) {
            this.btnsDef[btnName] = btnDef;
        },

        buildEditor: function () {
            var t = this,
                prefix = t.o.prefix,
                html = '';

            t.$box = $('<div/>', {
                class: prefix + 'box ' + prefix + 'editor-visible ' + prefix + t.o.lang + ' trumbowyg'
            });

            // $ta = Textarea
            // $ed = Editor
            t.isTextarea = t.$ta.is('textarea');
            if (t.isTextarea) {
                html = t.$ta.val();
                t.$ed = $('<div/>');
                t.$box
                    .insertAfter(t.$ta)
                    .append(t.$ed, t.$ta);
            } else {
                t.$ed = t.$ta;
                html = t.$ed.html();

                t.$ta = $('<textarea/>', {
                    name: t.$ta.attr('id'),
                    height: t.height
                }).val(html);

                t.$box
                    .insertAfter(t.$ed)
                    .append(t.$ta, t.$ed);
                t.syncCode();
            }

            t.$ta
                .addClass(prefix + 'textarea')
                .attr('tabindex', -1)
                ;

            t.$ed
                .addClass(prefix + 'editor')
                .attr({
                    contenteditable: true,
                    dir: t.lang._dir || 'ltr'
                })
                .html(html)
                ;

            if (t.o.tabindex) {
                t.$ed.attr('tabindex', t.o.tabindex);
            }

            if (t.$c.is('[placeholder]')) {
                t.$ed.attr('placeholder', t.$c.attr('placeholder'));
            }

            if (t.o.resetCss) {
                t.$ed.addClass(prefix + 'reset-css');
            }

            if (!t.o.autogrow) {
                t.$ta.add(t.$ed).css({
                    height: t.height
                });
            }

            t.semanticCode();


            var ctrl = false,
                composition = false,
                debounceButtonPaneStatus,
                updateEventName = t.isIE ? 'keyup' : 'input';

            t.$ed
                .on('dblclick', 'img', t.o.imgDblClickHandler)
                .on('keydown', function (e) {
                    if (e.ctrlKey) {
                        ctrl = true;
                        var key = t.keys[String.fromCharCode(e.which).toUpperCase()];

                        try {
                            t.execCmd(key.fn, key.param);
                            return false;
                        } catch (c) {
                        }
                    }
                })
                .on('compositionstart compositionupdate', function () {
                    composition = true;
                })
                .on(updateEventName + ' compositionend', function (e) {
                    if (e.type === 'compositionend') {
                        composition = false;
                    } else if (composition) {
                        return;
                    }

                    var keyCode = e.which;

                    if (keyCode >= 37 && keyCode <= 40) {
                        return;
                    }

                    if (e.ctrlKey && (keyCode === 89 || keyCode === 90)) {
                        t.$c.trigger('tbwchange');
                    } else if (!ctrl && keyCode !== 17) {
                        t.semanticCode(false, keyCode === 13);
                        t.$c.trigger('tbwchange');
                    } else if (typeof e.which === 'undefined') {
                        t.semanticCode(false, false, true);
                    }

                    setTimeout(function () {
                        ctrl = false;
                    }, 200);
                })
                .on('mouseup keydown keyup', function (e) {
                    if (e.ctrlKey)
                        ctrl = false;
                    clearTimeout(debounceButtonPaneStatus);
                    debounceButtonPaneStatus = setTimeout(function () {
                        t.updateButtonPaneStatus();
                    }, 50);
                })
                .on('focus blur', function (e) {
                    t.$c.trigger('tbw' + e.type);
                    if (e.type === 'blur') {
                        $('.' + prefix + 'active-button', t.$btnPane).removeClass(prefix + 'active-button ' + prefix + 'active');
                    }
                })
                .on('cut', function () {
                    setTimeout(function () {
                        t.semanticCode(false, true);
                        t.$c.trigger('tbwchange');
                    }, 0);
                })
                .on('paste', function (e) {
                    if (t.o.removeformatPasted) {
                        e.preventDefault();

                        try {
                            // IE
                            var text = window.clipboardData.getData('Text');

                            try {
                                // <= IE10
                                t.doc.selection.createRange().pasteHTML(text);
                            } catch (c) {
                                // IE 11
                                t.doc.getSelection().getRangeAt(0).insertNode(t.doc.createTextNode(text));
                            }
                        } catch (d) {
                            // Not IE
                            t.execCmd('insertText', (e.originalEvent || e).clipboardData.getData('text/plain'));
                        }
                    }

                    // Call pasteHandlers
                    $.each(t.pasteHandlers, function (i, pasteHandler) {
                        pasteHandler(e);
                    });

                    setTimeout(function () {
                        t.semanticCode(false, true);
                        t.$c.trigger('tbwpaste', e);
                    }, 0);
                });
            t.$ta.on('keyup paste', function () {
                t.$c.html(t.$ta.val());
                t.$c.trigger('tbwchange');
            });

            t.$box.on('keydown', function (e) {
                if (e.which === 27 && $('.' + prefix + 'modal-box', t.$box).length === 1) {
                    t.closeModal();
                    return false;
                }
            });
        },


        // Build button pane, use o.btns option
        buildBtnPane: function () {
            var t = this,
                prefix = t.o.prefix;

            var $btnPane = t.$btnPane = $('<div/>', {
                class: prefix + 'button-pane'
            });

            $.each(t.o.btns, function (i, btnGrps) {
                // Managment of group of buttons
                try {
                    var b = btnGrps.split('btnGrp-');
                    if (b[1] != null) {
                        btnGrps = t.o.btnsGrps[b[1]];
                    }
                } catch (c) {
                }

                if (!$.isArray(btnGrps)) {
                    btnGrps = [btnGrps];
                }

                var $btnGroup = $('<div/>', {
                    class: prefix + 'button-group ' + ((btnGrps.indexOf('fullscreen') >= 0) ? prefix + 'right' : '')
                });
                $.each(btnGrps, function (i, btn) {
                    try { // Prevent buildBtn error
                        var $item;

                        if (t.isSupportedBtn(btn)) { // It's a supported button
                            $item = t.buildBtn(btn);
                        }

                        $btnGroup.append($item);
                    } catch (c) {
                    }
                });
                $btnPane.append($btnGroup);
            });

            t.$box.prepend($btnPane);
        },


        // Build a button and his action
        buildBtn: function (btnName) { // btnName is name of the button
            var t = this,
                prefix = t.o.prefix,
                btn = t.btnsDef[btnName],
                isDropdown = btn.dropdown,
                hasIcon = btn.hasIcon != null ? btn.hasIcon : true,
                textDef = t.lang[btnName] || btnName,

                $btn = $('<button/>', {
                    type: 'button',
                    class: prefix + btnName + '-button ' + (btn.class || '') + (!hasIcon ? ' ' + prefix + 'textual-button' : ''),
                    html: t.hasSvg && hasIcon ?
                        '<svg><use xlink:href="' + t.svgPath + '#' + prefix + (btn.ico || btnName).replace(/([A-Z]+)/g, '-$1').toLowerCase() + '"/></svg>' :
                        t.hideButtonTexts ? '' : (btn.text || btn.title || t.lang[btnName] || btnName),
                    title: (btn.title || btn.text || textDef) + ((btn.key) ? ' (Ctrl + ' + btn.key + ')' : ''),
                    tabindex: -1,
                    mousedown: function () {
                        if (!isDropdown || $('.' + btnName + '-' + prefix + 'dropdown', t.$box).is(':hidden')) {
                            $('body', t.doc).trigger('mousedown');
                        }

                        if (t.$btnPane.hasClass(prefix + 'disable') && !$(this).hasClass(prefix + 'active') && !$(this).hasClass(prefix + 'not-disable')) {
                            return false;
                        }

                        t.execCmd((isDropdown ? 'dropdown' : false) || btn.fn || btnName, btn.param || btnName, btn.forceCss || false);

                        return false;
                    }
                });

            if (isDropdown) {
                $btn.addClass(prefix + 'open-dropdown');
                var dropdownPrefix = prefix + 'dropdown',
                    $dropdown = $('<div/>', { // the dropdown
                        class: dropdownPrefix + '-' + btnName + ' ' + dropdownPrefix + ' ' + prefix + 'fixed-top',
                        'data-dropdown': btnName
                    });
                $.each(isDropdown, function (i, def) {
                    if (t.btnsDef[def] && t.isSupportedBtn(def)) {
                        $dropdown.append(t.buildSubBtn(def));
                    }
                });
                t.$box.append($dropdown.hide());
            } else if (btn.key) {
                t.keys[btn.key] = {
                    fn: btn.fn || btnName,
                    param: btn.param || btnName
                };
            }

            if (!isDropdown) {
                t.tagToButton[(btn.tag || btnName).toLowerCase()] = btnName;
            }

            return $btn;
        },
        // Build a button for dropdown menu
        // @param n : name of the subbutton
        buildSubBtn: function (btnName) {
            var t = this,
                prefix = t.o.prefix,
                btn = t.btnsDef[btnName],
                hasIcon = btn.hasIcon != null ? btn.hasIcon : true;

            if (btn.key) {
                t.keys[btn.key] = {
                    fn: btn.fn || btnName,
                    param: btn.param || btnName
                };
            }

            t.tagToButton[(btn.tag || btnName).toLowerCase()] = btnName;

            return $('<button/>', {
                type: 'button',
                class: prefix + btnName + '-dropdown-button' + (btn.ico ? ' ' + prefix + btn.ico + '-button' : ''),
                html: t.hasSvg && hasIcon ? '<svg><use xlink:href="' + t.svgPath + '#' + prefix + (btn.ico || btnName).replace(/([A-Z]+)/g, '-$1').toLowerCase() + '"/></svg>' + (btn.text || btn.title || t.lang[btnName] || btnName) : (btn.text || btn.title || t.lang[btnName] || btnName),
                title: ((btn.key) ? ' (Ctrl + ' + btn.key + ')' : null),
                style: btn.style || null,
                mousedown: function () {
                    $('body', t.doc).trigger('mousedown');

                    t.execCmd(btn.fn || btnName, btn.param || btnName, btn.forceCss || false);

                    return false;
                }
            });
        },
        // Check if button is supported
        isSupportedBtn: function (b) {
            try {
                return this.btnsDef[b].isSupported();
            } catch (c) {
            }
            return true;
        },

        // Build overlay for modal box
        buildOverlay: function () {
            var t = this;
            t.$overlay = $('<div/>', {
                class: t.o.prefix + 'overlay'
            }).css({
                top: t.$btnPane.outerHeight(),
                height: (t.$ed.outerHeight() + 1) + 'px'
            }).appendTo(t.$box);
            return t.$overlay;
        },
        showOverlay: function () {
            var t = this;
            $(window).trigger('scroll');
            t.$overlay.fadeIn(200);
            t.$box.addClass(t.o.prefix + 'box-blur');
        },
        hideOverlay: function () {
            var t = this;
            t.$overlay.fadeOut(50);
            t.$box.removeClass(t.o.prefix + 'box-blur');
        },

        // Management of fixed button pane
        fixedBtnPaneEvents: function () {
            var t = this,
                fixedFullWidth = t.o.fixedFullWidth,
                $box = t.$box;

            if (!t.o.fixedBtnPane) {
                return;
            }

            t.isFixed = false;

            $(window)
                .on('scroll.' + t.eventNamespace + ' resize.' + t.eventNamespace, function () {
                    if (!$box) {
                        return;
                    }

                    t.syncCode();

                    var scrollTop = $(window).scrollTop(),
                        offset = $box.offset().top + 1,
                        bp = t.$btnPane,
                        oh = bp.outerHeight() - 2;

                    if ((scrollTop - offset > 0) && ((scrollTop - offset - t.height) < 0)) {
                        if (!t.isFixed) {
                            t.isFixed = true;
                            bp.css({
                                position: 'fixed',
                                top: 0,
                                left: fixedFullWidth ? '0' : 'auto',
                                zIndex: 7
                            });
                            $([t.$ta, t.$ed]).css({ marginTop: bp.height() });
                        }
                        bp.css({
                            width: fixedFullWidth ? '100%' : (($box.width() - 1) + 'px')
                        });

                        $('.' + t.o.prefix + 'fixed-top', $box).css({
                            position: fixedFullWidth ? 'fixed' : 'absolute',
                            top: fixedFullWidth ? oh : oh + (scrollTop - offset) + 'px',
                            zIndex: 15
                        });
                    } else if (t.isFixed) {
                        t.isFixed = false;
                        bp.removeAttr('style');
                        $([t.$ta, t.$ed]).css({ marginTop: 0 });
                        $('.' + t.o.prefix + 'fixed-top', $box).css({
                            position: 'absolute',
                            top: oh
                        });
                    }
                });
        },

        // Disable editor
        toggleDisable: function (disable) {
            var t = this,
                prefix = t.o.prefix;

            t.disabled = disable;

            if (disable) {
                t.$ta.attr('disabled', true);
            } else {
                t.$ta.removeAttr('disabled');
            }
            t.$box.toggleClass(prefix + 'disabled', disable);
            t.$ed.attr('contenteditable', !disable);
        },

        // Destroy the editor
        destroy: function () {
            var t = this,
                prefix = t.o.prefix,
                height = t.height;

            if (t.isTextarea) {
                t.$box.after(
                    t.$ta
                        .css({ height: height })
                        .val(t.html())
                        .removeClass(prefix + 'textarea')
                        .show()
                );
            } else {
                t.$box.after(
                    t.$ed
                        .css({ height: height })
                        .removeClass(prefix + 'editor')
                        .removeAttr('contenteditable')
                        .html(t.html())
                        .show()
                );
            }

            t.$ed.off('dblclick', 'img');

            t.destroyPlugins();

            t.$box.remove();
            t.$c.removeData('trumbowyg');
            $('body').removeClass(prefix + 'body-fullscreen');
            t.$c.trigger('tbwclose');
            $(window).off('scroll.' + t.eventNamespace + ' resize.' + t.eventNamespace);
        },


        // Empty the editor
        empty: function () {
            this.$ta.val('');
            this.syncCode(true);
        },


        // Function call when click on viewHTML button
        toggle: function () {
            var t = this,
                prefix = t.o.prefix;
            t.semanticCode(false, true);
            setTimeout(function () {
                t.doc.activeElement.blur();
                t.$box.toggleClass(prefix + 'editor-hidden ' + prefix + 'editor-visible');
                t.$btnPane.toggleClass(prefix + 'disable');
                $('.' + prefix + 'viewHTML-button', t.$btnPane).toggleClass(prefix + 'active');
                if (t.$box.hasClass(prefix + 'editor-visible')) {
                    t.$ta.attr('tabindex', -1);
                } else {
                    t.$ta.removeAttr('tabindex');
                }
            }, 0);
        },

        // Open dropdown when click on a button which open that
        dropdown: function (name) {
            var t = this,
                d = t.doc,
                prefix = t.o.prefix,
                $dropdown = $('[data-dropdown=' + name + ']', t.$box),
                $btn = $('.' + prefix + name + '-button', t.$btnPane),
                show = $dropdown.is(':hidden');

            $('body', d).trigger('mousedown');

            if (show) {
                var o = $btn.offset().left;
                $btn.addClass(prefix + 'active');

                $dropdown.css({
                    position: 'absolute',
                    top: $btn.offset().top - t.$btnPane.offset().top + $btn.outerHeight(),
                    left: (t.o.fixedFullWidth && t.isFixed) ? o + 'px' : (o - t.$btnPane.offset().left) + 'px'
                }).show();

                $(window).trigger('scroll');

                $('body', d).on('mousedown.' + t.eventNamespace, function (e) {
                    if (!$dropdown.is(e.target)) {
                        $('.' + prefix + 'dropdown', d).hide();
                        $('.' + prefix + 'active', d).removeClass(prefix + 'active');
                        $('body', d).off('mousedown.' + t.eventNamespace);
                    }
                });
            }
        },


        // HTML Code management
        html: function (html) {
            var t = this;
            if (html != null) {
                t.$ta.val(html);
                t.syncCode(true);
                return t;
            }
            return t.$ta.val();
        },
        syncTextarea: function () {
            var t = this;
            t.$ta.val(t.$ed.text().trim().length > 0 || t.$ed.find('hr,img,embed,iframe,input').length > 0 ? t.$ed.html() : '');
        },
        syncCode: function (force) {
            var t = this;
            if (!force && t.$ed.is(':visible')) {
                t.syncTextarea();
            } else {
                // wrap the content in a div it's easier to get the innerhtml
                var html = '<div>' + t.$ta.val() + '</div>';
                //scrub the html before loading into the doc
                html = $(t.o.tagsToRemove.join(','), html).remove().end().html();
                t.$ed.html(html);
            }

            if (t.o.autogrow) {
                t.height = t.$ed.height();
                if (t.height !== t.$ta.css('height')) {
                    t.$ta.css({ height: t.height });
                    t.$c.trigger('tbwresize');
                }
            }
        },

        // Analyse and update to semantic code
        // @param force : force to sync code from textarea
        // @param full  : wrap text nodes in <p>
        // @param keepRange  : leave selection range as it is
        semanticCode: function (force, full, keepRange) {
            var t = this;
            t.saveRange();
            t.syncCode(force);

            if (t.o.semantic) {
                t.semanticTag('b', 'strong');
                t.semanticTag('i', 'em');

                if (full) {
                    var inlineElementsSelector = t.o.inlineElementsSelector,
                        blockElementsSelector = ':not(' + inlineElementsSelector + ')';

                    // Wrap text nodes in span for easier processing
                    t.$ed.contents().filter(function () {
                        return this.nodeType === 3 && this.nodeValue.trim().length > 0;
                    }).wrap('<span data-tbw/>');

                    // Wrap groups of inline elements in paragraphs (recursive)
                    var wrapInlinesInParagraphsFrom = function ($from) {
                        if ($from.length !== 0) {
                            var $finalParagraph = $from.nextUntil(blockElementsSelector).addBack().wrapAll('<p/>').parent(),
                                $nextElement = $finalParagraph.nextAll(inlineElementsSelector).first();
                            $finalParagraph.next('br').remove();
                            wrapInlinesInParagraphsFrom($nextElement);
                        }
                    };
                    wrapInlinesInParagraphsFrom(t.$ed.children(inlineElementsSelector).first());

                    t.semanticTag('div', 'p', true);

                    // Unwrap paragraphs content, containing nothing usefull
                    t.$ed.find('p').filter(function () {
                        // Don't remove currently being edited element
                        if (t.range && this === t.range.startContainer) {
                            return false;
                        }
                        return $(this).text().trim().length === 0 && $(this).children().not('br,span').length === 0;
                    }).contents().unwrap();

                    // Get rid of temporial span's
                    $('[data-tbw]', t.$ed).contents().unwrap();

                    // Remove empty <p>
                    t.$ed.find('p:empty').remove();
                }

                if (!keepRange) {
                    t.restoreRange();
                }

                t.syncTextarea();
            }
        },

        semanticTag: function (oldTag, newTag, copyAttributes) {
            $(oldTag, this.$ed).each(function () {
                var $oldTag = $(this);
                $oldTag.wrap('<' + newTag + '/>');
                if (copyAttributes) {
                    $.each($oldTag.prop('attributes'), function () {
                        $oldTag.parent().attr(this.name, this.value);
                    });
                }
                $oldTag.contents().unwrap();
            });
        },

        // Function call when user click on "Insert Link"
        createLink: function () {
            var t = this,
                documentSelection = t.doc.getSelection(),
                node = documentSelection.focusNode,
                url,
                title,
                target;

            while (['A', 'DIV'].indexOf(node.nodeName) < 0) {
                node = node.parentNode;
            }

            if (node && node.nodeName === 'A') {
                var $a = $(node);
                url = $a.attr('href');
                title = $a.attr('title');
                target = $a.attr('target');
                var range = t.doc.createRange();
                range.selectNode(node);
                documentSelection.removeAllRanges();
                documentSelection.addRange(range);
            }

            t.saveRange();

            t.openModalInsert(t.lang.createLink, {
                url: {
                    label: 'URL',
                    required: true,
                    value: url
                },
                title: {
                    label: t.lang.title,
                    value: title
                },
                text: {
                    label: t.lang.text,
                    value: t.getRangeText()
                },
                target: {
                    label: t.lang.target,
                    value: target
                }
            }, function (v) { // v is value
                var link = $(['<a href="', v.url, '">', v.text, '</a>'].join(''));
                if (v.title.length > 0) {
                    link.attr('title', v.title);
                }
                if (v.target.length > 0) {
                    link.attr('target', v.target);
                }
                t.range.deleteContents();
                t.range.insertNode(link[0]);
                return true;
            });
        },
        unlink: function () {
            var t = this,
                documentSelection = t.doc.getSelection(),
                node = documentSelection.focusNode;

            if (documentSelection.isCollapsed) {
                while (['A', 'DIV'].indexOf(node.nodeName) < 0) {
                    node = node.parentNode;
                }

                if (node && node.nodeName === 'A') {
                    var range = t.doc.createRange();
                    range.selectNode(node);
                    documentSelection.removeAllRanges();
                    documentSelection.addRange(range);
                }
            }
            t.execCmd('unlink', undefined, undefined, true);
        },
        insertImage: function () {
            var t = this;
            t.saveRange();
            t.openModalInsert(t.lang.insertImage, {
                url: {
                    label: 'URL',
                    required: true
                },
                alt: {
                    label: t.lang.description,
                    value: t.getRangeText()
                }
            }, function (v) { // v are values
                t.execCmd('insertImage', v.url);
                $('img[src="' + v.url + '"]:not([alt])', t.$box).attr('alt', v.alt);
                return true;
            });
        },
        fullscreen: function () {
            var t = this,
                prefix = t.o.prefix,
                fullscreenCssClass = prefix + 'fullscreen',
                isFullscreen;

            t.$box.toggleClass(fullscreenCssClass);
            isFullscreen = t.$box.hasClass(fullscreenCssClass);
            $('body').toggleClass(prefix + 'body-fullscreen', isFullscreen);
            $(window).trigger('scroll');
            t.$c.trigger('tbw' + (isFullscreen ? 'open' : 'close') + 'fullscreen');
        },


        /*
         * Call method of trumbowyg if exist
         * else try to call anonymous function
         * and finaly native execCommand
         */
        execCmd: function (cmd, param, forceCss, skipTrumbowyg) {
            var t = this;
            skipTrumbowyg = !!skipTrumbowyg || '';

            if (cmd !== 'dropdown') {
                t.$ed.focus();
            }

            try {
                t.doc.execCommand('styleWithCSS', false, forceCss || false);
            } catch (c) {
            }

            try {
                t[cmd + skipTrumbowyg](param);
            } catch (c) {
                try {
                    cmd(param);
                } catch (e2) {
                    if (cmd === 'insertHorizontalRule') {
                        param = undefined;
                    } else if (cmd === 'formatBlock' && t.isIE) {
                        param = '<' + param + '>';
                    }

                    t.doc.execCommand(cmd, false, param);

                    t.syncCode();
                    t.semanticCode(false, true);
                }

                if (cmd !== 'dropdown') {
                    t.updateButtonPaneStatus();
                    t.$c.trigger('tbwchange');
                }
            }
        },


        // Open a modal box
        openModal: function (title, content) {
            var t = this,
                prefix = t.o.prefix;

            // No open a modal box when exist other modal box
            if ($('.' + prefix + 'modal-box', t.$box).length > 0) {
                return false;
            }

            t.saveRange();
            t.showOverlay();

            // Disable all btnPane btns
            t.$btnPane.addClass(prefix + 'disable');

            // Build out of ModalBox, it's the mask for animations
            var $modal = $('<div/>', {
                class: prefix + 'modal ' + prefix + 'fixed-top'
            }).css({
                top: t.$btnPane.height()
            }).appendTo(t.$box);

            // Click on overlay close modal by cancelling them
            t.$overlay.one('click', function () {
                $modal.trigger('tbwcancel');
                return false;
            });

            // Build the form
            var $form = $('<form/>', {
                action: '',
                html: content
            })
                .on('submit', function () {
                    $modal.trigger('tbwconfirm');
                    return false;
                })
                .on('reset', function () {
                    $modal.trigger('tbwcancel');
                    return false;
                });


            // Build ModalBox and animate to show them
            var $box = $('<div/>', {
                class: prefix + 'modal-box',
                html: $form
            })
                .css({
                    top: '-' + t.$btnPane.outerHeight() + 'px',
                    opacity: 0
                })
                .appendTo($modal)
                .animate({
                    top: 0,
                    opacity: 1
                }, 100);


            // Append title
            $('<span/>', {
                text: title,
                class: prefix + 'modal-title'
            }).prependTo($box);

            $modal.height($box.outerHeight() + 10);


            // Focus in modal box
            $('input:first', $box).focus();


            // Append Confirm and Cancel buttons
            t.buildModalBtn('submit', $box);
            t.buildModalBtn('reset', $box);


            $(window).trigger('scroll');

            return $modal;
        },
        // @param n is name of modal
        buildModalBtn: function (n, $modal) {
            var t = this,
                prefix = t.o.prefix;

            return $('<button/>', {
                class: prefix + 'modal-button ' + prefix + 'modal-' + n,
                type: n,
                text: t.lang[n] || n
            }).appendTo($('form', $modal));
        },
        // close current modal box
        closeModal: function () {
            var t = this,
                prefix = t.o.prefix;

            t.$btnPane.removeClass(prefix + 'disable');
            t.$overlay.off();

            // Find the modal box
            var $modalBox = $('.' + prefix + 'modal-box', t.$box);

            $modalBox.animate({
                top: '-' + $modalBox.height()
            }, 100, function () {
                $modalBox.parent().remove();
                t.hideOverlay();
            });

            t.restoreRange();
        },
        // Preformated build and management modal
        openModalInsert: function (title, fields, cmd) {
            var t = this,
                prefix = t.o.prefix,
                lg = t.lang,
                html = '',
                CONFIRM_EVENT = 'tbwconfirm';

            $.each(fields, function (fieldName, field) {
                var l = field.label,
                    n = field.name || fieldName,
                    a = field.attributes || {};

                var attr = Object.keys(a).map(function (prop) {
                    return prop + '="' + a[prop] + '"';
                }).join(' ');

                html += '<label><input type="' + (field.type || 'text') + '" name="' + n + '" value="' + (field.value || '').replace(/"/g, '&quot;') + '"' + attr + '><span class="' + prefix + 'input-infos"><span>' +
                    ((!l) ? (lg[fieldName] ? lg[fieldName] : fieldName) : (lg[l] ? lg[l] : l)) +
                    '</span></span></label>';
            });

            return t.openModal(title, html)
                .on(CONFIRM_EVENT, function () {
                    var $form = $('form', $(this)),
                        valid = true,
                        values = {};

                    $.each(fields, function (fieldName, field) {
                        var $field = $('input[name="' + fieldName + '"]', $form),
                            inputType = $field.attr('type');

                        if (inputType.toLowerCase() === 'checkbox') {
                            values[fieldName] = $field.is(':checked');
                        } else {
                            values[fieldName] = $.trim($field.val());
                        }
                        // Validate value
                        if (field.required && values[fieldName] === '') {
                            valid = false;
                            t.addErrorOnModalField($field, t.lang.required);
                        } else if (field.pattern && !field.pattern.test(values[fieldName])) {
                            valid = false;
                            t.addErrorOnModalField($field, field.patternError);
                        }
                    });

                    if (valid) {
                        t.restoreRange();

                        if (cmd(values, fields)) {
                            t.syncCode();
                            t.$c.trigger('tbwchange');
                            t.closeModal();
                            $(this).off(CONFIRM_EVENT);
                        }
                    }
                })
                .one('tbwcancel', function () {
                    $(this).off(CONFIRM_EVENT);
                    t.closeModal();
                });
        },
        addErrorOnModalField: function ($field, err) {
            var prefix = this.o.prefix,
                $label = $field.parent();

            $field
                .on('change keyup', function () {
                    $label.removeClass(prefix + 'input-error');
                });

            $label
                .addClass(prefix + 'input-error')
                .find('input+span')
                .append(
                $('<span/>', {
                    class: prefix + 'msg-error',
                    text: err
                })
                );
        },


        // Range management
        saveRange: function () {
            var t = this,
                documentSelection = t.doc.getSelection();

            t.range = null;

            if (documentSelection.rangeCount) {
                var savedRange = t.range = documentSelection.getRangeAt(0),
                    range = t.doc.createRange(),
                    rangeStart;
                range.selectNodeContents(t.$ed[0]);
                range.setEnd(savedRange.startContainer, savedRange.startOffset);
                rangeStart = (range + '').length;
                t.metaRange = {
                    start: rangeStart,
                    end: rangeStart + (savedRange + '').length
                };
            }
        },
        restoreRange: function () {
            var t = this,
                metaRange = t.metaRange,
                savedRange = t.range,
                documentSelection = t.doc.getSelection(),
                range;

            if (!savedRange) {
                return;
            }

            if (metaRange && metaRange.start !== metaRange.end) { // Algorithm from http://jsfiddle.net/WeWy7/3/
                var charIndex = 0,
                    nodeStack = [t.$ed[0]],
                    node,
                    foundStart = false,
                    stop = false;

                range = t.doc.createRange();

                while (!stop && (node = nodeStack.pop())) {
                    if (node.nodeType === 3) {
                        var nextCharIndex = charIndex + node.length;
                        if (!foundStart && metaRange.start >= charIndex && metaRange.start <= nextCharIndex) {
                            range.setStart(node, metaRange.start - charIndex);
                            foundStart = true;
                        }
                        if (foundStart && metaRange.end >= charIndex && metaRange.end <= nextCharIndex) {
                            range.setEnd(node, metaRange.end - charIndex);
                            stop = true;
                        }
                        charIndex = nextCharIndex;
                    } else {
                        var cn = node.childNodes,
                            i = cn.length;

                        while (i > 0) {
                            i -= 1;
                            nodeStack.push(cn[i]);
                        }
                    }
                }
            }

            documentSelection.removeAllRanges();
            documentSelection.addRange(range || savedRange);
        },
        getRangeText: function () {
            return this.range + '';
        },

        updateButtonPaneStatus: function () {
            var t = this,
                prefix = t.o.prefix,
                tags = t.getTagsRecursive(t.doc.getSelection().focusNode),
                activeClasses = prefix + 'active-button ' + prefix + 'active';

            $('.' + prefix + 'active-button', t.$btnPane).removeClass(activeClasses);
            $.each(tags, function (i, tag) {
                var btnName = t.tagToButton[tag.toLowerCase()],
                    $btn = $('.' + prefix + btnName + '-button', t.$btnPane);

                if ($btn.length > 0) {
                    $btn.addClass(activeClasses);
                } else {
                    try {
                        $btn = $('.' + prefix + 'dropdown .' + prefix + btnName + '-dropdown-button', t.$box);
                        var dropdownBtnName = $btn.parent().data('dropdown');
                        $('.' + prefix + dropdownBtnName + '-button', t.$box).addClass(activeClasses);
                    } catch (e) {
                    }
                }
            });
        },
        getTagsRecursive: function (element, tags) {
            var t = this;
            tags = tags || (element && element.tagName ? [element.tagName] : []);

            if (element && element.parentNode) {
                element = element.parentNode;
            } else {
                return tags;
            }

            var tag = element.tagName;
            if (tag === 'DIV') {
                return tags;
            }
            if (tag === 'P' && element.style.textAlign !== '') {
                tags.push(element.style.textAlign);
            }

            $.each(t.tagHandlers, function (i, tagHandler) {
                tags = tags.concat(tagHandler(element, t));
            });

            tags.push(tag);

            return t.getTagsRecursive(element, tags);
        },

        // Plugins
        initPlugins: function () {
            var t = this;
            t.loadedPlugins = [];
            $.each($.trumbowyg.plugins, function (name, plugin) {
                if (!plugin.shouldInit || plugin.shouldInit(t)) {
                    plugin.init(t);
                    if (plugin.tagHandler) {
                        t.tagHandlers.push(plugin.tagHandler);
                    }
                    t.loadedPlugins.push(plugin);
                }
            });
        },
        destroyPlugins: function () {
            $.each(this.loadedPlugins, function (i, plugin) {
                if (plugin.destroy) {
                    plugin.destroy();
                }
            });
        }
    };
})(navigator, window, document, jQuery);
